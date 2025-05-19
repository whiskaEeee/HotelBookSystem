function fnLoadHotelList() {

        $('.loading').show();

        var ObjData = {
            NumberOfNights: $('#NumberOfNights').val(),
            CheckInDate: $('#CheckInDate').val()
        };

        $.ajax({
            type: "POST",
            url: getHotelsUrl,
            data: ObjData,
            success: function (data) {
                $('#hotelList').empty();
                $('#hotelList').html(data);
                $('.loading').hide();
            },
            failure: function(response) {
                $('.loading').hide();
                alert(response.responseText);
            },
            error: function(response) {
                $('.loading').hide();
                alert(response.responseText);
            }
        });
    }