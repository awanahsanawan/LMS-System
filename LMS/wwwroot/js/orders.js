var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("approved")) {
        loadDataTable("GetAllApproved");

    } else {
        if (url.includes("pending")) {
            loadDataTable("GetAllPending");

        }
        else {
            loadDataTable("GetAll");
        }
    }
   
 
});

function loadDataTable(url) {

    dataTable = $('#tblData').dataTable({
        "ajax": {
            "url": "/admin/order/"+url,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "phone", "width": "20%" },
            { "data": "email", "width": "20%" },
            { "data": "status", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Admin/order/Details/${data
                        }" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                        <i class='far fa-edit'></i> Details
                        </a>
                        &nbsp; <a onclick=Delete("/Admin/category/Delete/${data
                        }") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                        <i class='far fa-trash-alt'></i> Delete
                        </a>
                        </div>
                        `;
                }, "width": "15%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        },
        "width": "100%"
    });
}

