var searchTimer;
var searchResultsUl = document.getElementById('searchResults');

document.getElementById('search').addEventListener('submit', function (event) {
    event.preventDefault();
});
function handleSearch(event) {
    clearTimeout(searchTimer);
    var searchKeyword = event.target.value.trim();
    if (searchKeyword !== '') {
        searchTimer = setTimeout(function () {
            fetchSearchResults(searchKeyword);
        }, 500); // Đợi 500ms sau khi ngừng nhập để gửi yêu cầu tìm kiếm
    } else {
        searchResultsUl.innerHTML = '';
    }
}

function fetchSearchResults(keyword) {
    searchResultsUl.innerHTML = '';

    fetch('/Search/GetSearchResults?keyword=' + keyword)
        .then(response => response.json())
        .then(data => {
            if (data.length > 0) {
                data.forEach(item => {
                    var li = document.createElement('li');
                    var link = document.createElement('a');
                    link.href = '/ListMovie/Detail?id=' + item.id;
                    link.textContent = item.name;
                    li.appendChild(link);
                    searchResultsUl.appendChild(li);
                });
                searchResultsUl.style.display = 'block'; // Hiển thị danh sách kết quả tìm kiếm
            } else {
                searchResultsUl.style.display = 'none'; // Ẩn danh sách nếu không có kết quả
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

document.addEventListener('click', function (event) {
    if (!searchResultsUl.contains(event.target)) {
        searchResultsUl.style.display = 'none'; // Ẩn danh sách khi nhấp chuột ra ngoài
    }
});


$(document).ready(function () {
    $.ajax({
        url: '@Url.Action("GetDanhMuc", "DanhMuc")',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dropdownMenu = $('.dropdown-menu-dm');
            $.each(data, function (index, value) {
                dropdownMenu.append('<a href="#">' + value + '</a>');
            });
        }
    });
});
