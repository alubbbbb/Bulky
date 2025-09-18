
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:'/admin/product/getall' },
    }   "columns": [
        { data: 'titel', "width": "15%" },
        { data: 'isbn', "width": "15%" },
        { data: 'listPrice', "width": "15%" },
        { data: 'author', "width": "15%" }
        { data: 'category.name', "width": "15%" }
    ]
    });
}

