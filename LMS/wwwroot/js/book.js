
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').dataTable({

        "ajax": {
            "url": "/admin/book/GetAll",
            "type": "GET",
            "datatype": "json",
            "dataSrc": ""

        },
        "columns": [
            { "data": "title", "width": "10%" },
            { "data": "category.name", "width": "10%" },
            { "data": "price", "width": "10%" },
            { "data": "quantity", "width": "10%" },
            { "data": "edition", "width": "10%" },
            { "data": "author.name", "width": "10%" },
           
            




            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Admin/book/Upsert/${data
                        }" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                        <i class='far fa-edit'></i> Edit
                        </a>
                        &nbsp; <a onclick=Delete("/Admin/book/Delete/${data
                        }") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                        <i class='far fa-trash-alt'></i> Delete
                        </a>
                        </div>
                        `;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you you want to delete?",
        text: "You will not be able to restore the content!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6b55",
        confirmButtonText: "Yes, deleted it!",
        closeOnconfirm: true

    },
        function () {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {

                        toastr.success(data.message);
                        dataTable.api().ajax.reload();
                        // dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        });
}

function ShowMessage(msg) {

}