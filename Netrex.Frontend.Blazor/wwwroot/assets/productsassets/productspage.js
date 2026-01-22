function changeImg(src, thumb) {

    
    const mainImg = document.querySelector('.main-img-container img');
    mainImg.src = src;

    
    document.querySelectorAll('.thumb-list .thumb').forEach(t => {
        t.classList.remove('active');
    });

   
    thumb.classList.add('active');
}

function updateQty(val) {
    const qtyInput = document.getElementById('qty');
    let current = parseInt(qtyInput.value);
    current += val;
    if (current < 1) current = 1;
    qtyInput.value = current;
}
function selectSize(btn) {
    document.querySelectorAll('.size-btn').forEach(b => {
        b.classList.remove('active');
    });

    btn.classList.add('active');
}

