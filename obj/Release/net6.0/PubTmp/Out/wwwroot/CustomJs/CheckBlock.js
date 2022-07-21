$("#txtBlock").blur(function () {
    $.ajax({
        type: "GET",
        url: "/api/Block",
        data: { Districtid: $("#ddldistname").val(), BlockName: $("#txtBlock").val() },
        contentType: "application/json",
        dataType: "text",
        async: true,
        success: function (response) {            
            if (response == "NA") {
                $("#Add").hide();
                Swal.fire({
                    icon: 'error',
                    title: 'Error...',
                    text: 'Block Already Exist'
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
