using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Netrex.Frontend.Application.Commons.Enums;
using Netrex.Frontend.Application.ViewModels.Common;

namespace Netrex.Frontend.Blazor.Components.Layout
{
    public partial class Toast
    {
        // Internal helper class to track the timer for each toast
        private class ToastRuntime : IDisposable
        {
            public ToastMessage Message { get; set; } = null!;
            public CancellationTokenSource TokenSource { get; set; } = new();
            public DateTime StartTime { get; set; }
            public DateTime? PauseTime { get; set; }
            public int ElapsedTime { get; set; } // Total elapsed time in milliseconds (excluding paused time)
            public bool IsExiting { get; set; } = false; // Flag for exit animation
            private bool _disposed = false;

            public void Dispose()
            {
                if (!_disposed)
                {
                    TokenSource?.Cancel();
                    TokenSource?.Dispose();
                    _disposed = true;
                }
            }
        }

        // Using a Dictionary for faster lookup via ID with thread-safe access
        private readonly Dictionary<Guid, ToastRuntime> ActiveToasts = new();
        private readonly object _lockObject = new object();

        protected override void OnInitialized() => _toastService.OnShow += HandleShow;

        private void HandleShow(ToastMessage message)
        {
            InvokeAsync(async () =>
            {
                lock (_lockObject)
                {
                    // Prevent duplicate toasts with same ID
                    if (ActiveToasts.ContainsKey(message.Id))
                        return;

                    var runtime = new ToastRuntime
                    {
                        Message = message,
                        StartTime = DateTime.UtcNow,
                        ElapsedTime = 0
                    };
                    ActiveToasts.Add(message.Id, runtime);
                }

                // Play sound if enabled
                if (message.PlaySound)
                {
                    await PlayToastSound(message.Type, message.SoundVolume);
                }

                StateHasChanged();
                await StartTimer(message.Id);
            });
        }

        private async Task PlayToastSound(ToastType type, double volume)
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("toastSounds.playSound", type.ToString(), volume);
            }
            catch (JSException)
            {
                // Silently fail if JavaScript is not available or sound system not initialized
            }
            catch (Exception)
            {
                // Silently fail on any other error
            }
        }

        private async Task StartTimer(Guid toastId)
        {
            ToastRuntime? runtime;
            lock (_lockObject)
            {
                if (!ActiveToasts.TryGetValue(toastId, out runtime) || runtime == null)
                    return;
            }

            try
            {
                // Calculate remaining duration
                int remainingDuration = runtime.Message.Duration - runtime.ElapsedTime;

                // If no time remaining, remove immediately
                if (remainingDuration <= 0)
                {
                    RemoveToast(toastId);
                    return;
                }

                // Wait for the remaining duration, but stop if TokenSource is cancelled
                await Task.Delay(remainingDuration, runtime.TokenSource.Token);
                RemoveToast(toastId);
            }
            catch (TaskCanceledException)
            {
                // The mouse hovered or toast was removed, so the task was cancelled. Do nothing.
            }
        }

        private void PauseToast(ToastRuntime runtime)
        {
            lock (_lockObject)
            {
                if (runtime != null && !runtime.TokenSource.Token.IsCancellationRequested)
                {
                    // Only calculate elapsed time if not already paused
                    if (runtime.PauseTime == null)
                    {
                        // Calculate elapsed time from start (or last resume)
                        var now = DateTime.UtcNow;
                        var elapsedSinceStart = (int)(now - runtime.StartTime).TotalMilliseconds;
                        runtime.ElapsedTime += elapsedSinceStart;
                        runtime.PauseTime = now;
                    }

                    runtime.TokenSource.Cancel();
                }
            }
        }

        private void ResumeToast(ToastRuntime runtime)
        {
            lock (_lockObject)
            {
                if (runtime != null && ActiveToasts.ContainsKey(runtime.Message.Id))
                {
                    // Update elapsed time if there was a pause
                    if (runtime.PauseTime.HasValue)
                    {
                        var now = DateTime.UtcNow;
                        var pausedDuration = (int)(now - runtime.PauseTime.Value).TotalMilliseconds;
                        // Don't add paused time to elapsed time - we only count active time
                        runtime.PauseTime = null;
                    }

                    // Check if elapsed time already exceeds duration
                    if (runtime.ElapsedTime >= runtime.Message.Duration)
                    {
                        RemoveToast(runtime.Message.Id);
                        return;
                    }

                    // Update start time to now for calculating elapsed time on next pause
                    runtime.StartTime = DateTime.UtcNow;

                    // Dispose old token source and create new one
                    runtime.TokenSource.Dispose();
                    runtime.TokenSource = new CancellationTokenSource();

                    _ = StartTimer(runtime.Message.Id);
                }
            }
        }

        private void RemoveToast(Guid id)
        {
            ToastRuntime? runtime = null;
            lock (_lockObject)
            {
                if (ActiveToasts.TryGetValue(id, out runtime) && runtime != null)
                {
                    // Start exit animation
                    runtime.IsExiting = true;
                }
            }

            if (runtime != null)
            {
                // Update UI to show exit animation
                StateHasChanged();

                // Wait for exit animation to complete, then remove
                InvokeAsync(async () =>
                {
                    await Task.Delay(300); // Match exit animation duration

                    lock (_lockObject)
                    {
                        if (ActiveToasts.Remove(id, out var runtimeToDispose))
                        {
                            runtimeToDispose?.Dispose();
                        }
                    }

                    StateHasChanged();
                });
            }
        }

        private void HandleKeyDown(KeyboardEventArgs e, Guid toastId)
        {
            // Allow closing toast with Escape key for keyboard accessibility
            if (e.Key == "Escape" || e.Key == "Enter")
            {
                RemoveToast(toastId);
            }
        }

        private string GetIconClass(ToastType type) => type switch
        {
            ToastType.Cart => "bi bi-cart",
            ToastType.Payment => "bi bi-credit-card",
            ToastType.Order => "bi bi-box-seam",
            ToastType.Shipping => "bi bi-truck",
            ToastType.Wishlist => "bi bi-heart",
            ToastType.Review => "bi bi-star",
            ToastType.Discount => "bi bi-percent",
            ToastType.Stock => "bi bi-box",
            ToastType.Notification => "bi bi-bell",
            _ => string.Empty
        };

        private string GetDefaultIcon(ToastType type) => type switch
        {
            ToastType.Success => "✓",
            ToastType.Error => "✕",
            ToastType.Warning => "⚠",
            _ => "ℹ"
        };

        public void Dispose()
        {
            _toastService.OnShow -= HandleShow;

            // Dispose all active toast runtimes to prevent memory leaks
            lock (_lockObject)
            {
                foreach (var runtime in ActiveToasts.Values)
                {
                    runtime.Dispose();
                }
                ActiveToasts.Clear();
            }
        }
    }
}
