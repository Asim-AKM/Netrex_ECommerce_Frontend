// Services/WishList/WishListStateService.cs
namespace Netrex.Frontend.Application.Services.WishList
{
    public class WishListStateService
    {
        private int _count = 0;
        public int Count => _count;

        // Action nahi — sirf data store karo
        public event Action? OnCountChanged;

        public void SetCount(int count)
        {
            _count = count;
            OnCountChanged?.Invoke(); 
        }

        public void Increment()
        {
            _count++;
            OnCountChanged?.Invoke();
        }

        public void Decrement()
        {
            if (_count > 0) _count--;
            OnCountChanged?.Invoke();
        }
    }
}