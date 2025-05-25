var dataTable;

$(function () {
    const urlParams = new URLSearchParams(window.location.search);
    const status = urlParams.get('status');
    loadDataTable(status);
});

function loadDataTable(status) {
    dataTable = $('#bookingsTable').DataTable({
        "ajax": {
            url: "/Booking/getall?status=" + status,
        },
        "columns": [
            { data: "id" },
            { data: "name" },
            { data: "phoneNumber" },
            { data: "email" },
            { data: "status" },
            { data: "checkInDate" },
            { data: "numberOfNights" },
            { data: "totalCost", render: $.fn.dataTable.render.number('.', ',', 2) },
            {
                data: "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group">
                                <a href="/Booking/BookingDetails?bookingId=${data}" class="btn btn-warning mx-2">
                                    <i class="bi bi-pencil-square"></i> Details
                                </a>
                            </div>`
                }
            }
        ]
    })
}
