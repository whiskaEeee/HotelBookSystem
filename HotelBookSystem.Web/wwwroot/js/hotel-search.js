function fnLoadHotelList() {

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
            }
        });
    }