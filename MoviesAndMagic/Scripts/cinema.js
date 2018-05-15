//$(document).ready(function () {

////    $("#userValidation").on("click", function () {
////        var FirstName = $("#Firstname").val();
////        var LastName= $("#Lastname").val();
////        var Email = $("#Email").val();

////        $.ajax({
////            url: '/user/create',
////            type: 'POST',
////            data: { FirstName: FirstName, LastName: LastName, Email: Email, Password: Password }
////            ,success: function (result) {

////                if (result === false) {
////                    alert("This user is alreeady added in the database");
////                } else {
////                    alert("Ai adaugat cu succes!");
////                    location.reload();
////                }
////            }
////        });

////    });
    
//    function getAllReservation() {
//        //jquery.support.cors = true;
//        $.ajax({
//            url: url,
//            type: 'get',
//            datatype: 'json',
//            success: function (data) {
//                WriteResponses(data);
//            },
//            error: function (x, y, z) {
//                alert(x + '\n' + y + '\n' + z);
//            }
//        });
//    }
//    function UpdatePlayer() {
//        var reservation = {
//            Id: $('#newReserationId').val(),
//            SeatNo: $('#SeatId').val(),
//            ShowId: $('#ShowId').val()
           
//        };

//        $.ajax({
//            url: 'Reservation/Edit' + '/id',
//            type: 'PUT',
//            data: { Id: Id, SeatNo: SeatId, ShowId: ShowId, },
//            success: function (result) {
//                if (result === true) {
//                    alert("Ai modificat!");
//                } else {
//                    alert("Aceasta inregistrare exista in baza dedate!\nSAU\nNu ai modificat nimic!");
//                }
//            }
//        });
//    }
//});