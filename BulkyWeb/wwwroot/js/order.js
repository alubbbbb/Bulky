var dataTable;
var status = ""; // Status-Variable definieren

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/order/getall?status=' + status,
            error: function (xhr, error, thrown) {
                console.error('Fehler beim Laden der Daten:', error);
            }
        },
        "columns": [
            { data: 'id', width: "5%" },
            { data: 'name', width: "25%" },
            { data: 'phoneNumber', width: "20%" },
            { data: 'applicationUser.email', width: "20%" },
            { data: 'orderStatus', width: "10%" },
            { data: 'orderTotal', width: "10%" },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/order/details?orderId=${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                        </div>
                    `;
                },
                width: "10%"
            }
        ]
    });
}


