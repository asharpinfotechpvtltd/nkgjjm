$("#ddlblock").change(function () {    
    $("#ddlGramPanchyaat").empty();
    $.ajax({
        type: "GET",
        url: "/api/GramPanchayats",
        data: { id: $("#ddlblock").val() },
        contentType: "application/json",
        dataType: "json",
        async: true,
        success: function (response) {
            console.log(response);
            $("#ddlGramPanchyaat").prepend("<option value='' selected='selected'>Select Gram Panchaayat</option>");
            $.each(response, function (i, item) {
                var html = '';
                html += "<option value=" + item.id + ">" + item.gramPanchayat + "</option>";
                $("#ddlGramPanchyaat").append(html);

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
