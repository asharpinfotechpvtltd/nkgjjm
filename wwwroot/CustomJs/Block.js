$("#ddldistname").change(function () {
    $("#ddlblock").empty();    
    $.ajax({
        type: "GET",
        url: "/api/DistBlocks",
        data: { id: $("#ddldistname").val() },
        contentType: "application/json",
        dataType: "json",
        async: true,
        success: function (response) {
            console.log(response);
            $("#ddlblock").prepend("<option value='' selected='selected'>Select Block</option>");
            $.each(response, function (i, item) {
                var html = '';
                html += "<option value=" + item.id + ">" + item.block + "</option>";
                $("#ddlblock").append(html);

            })
        },
        failure: function (response) {
            console.log(response.responseText);
        },
        error: function (response) {
            console.log(response.responseText);
        }
    });
});
