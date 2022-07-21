$("#txtdist").blur(function () {
    $.ajax({
        type: "GET",
        url: "/api/District",
        data: { DistrictName: $("#txtdist").val() },
        contentType: "application/json",
        dataType: "text",
        async: true,
        success: function (response) {
            if (response == "NA") {
                $("#Add").hide();
                Swal.fire({
                    icon: 'error',
                    title: 'Error...',
                    text: 'District Already Exist'
                })
            }
            else {
                $("#Add").show();
            }


        },
        failure: function (response) {
            console.log(response.responseText);
        },
        error: function (response) {
            console.log(response.responseText);
        }
    });
});
