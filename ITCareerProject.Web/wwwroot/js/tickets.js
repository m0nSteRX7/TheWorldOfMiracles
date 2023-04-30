$(document).on("click",
    '.buy-ticket',
    function () {
        var ticketId = Number($(this).attr('data-id'));

        Swal.fire({
            title: 'Do you want to buy a ticket for this event?',
            showDenyButton: true,
            confirmButtonText: 'Yes',
            denyButtonText: `No`,
        }).then((result) => {
            if (!result.isConfirmed) return;

            $.ajax({
                type: "POST",
                url: `${window.location.origin}/Tickets/BuyTicket`,
                data: { id: ticketId },
                dataType: "json",
                error: function (event, jqxhr, settings, thrownError) {
                    if (event.status != 200) {
                        Swal.fire(
                            'Error!',
                            event.responseText.split(': ')[1].split("\r\n")[0],
                            'error'
                        );
                    } else {
                        Swal.fire(
                            'Success!',
                            "Ticket bought successfully !",
                            'success'
                        ).then(() => window.location.reload());
                    }
                }
            });
        });
    });

$(document).on("click",
    '.delete-ticket',
    function () {
        var ticketId = $(this).attr('data-id');

        Swal.fire({
            title: 'Do you want to delete your ticket for this event?',
            showDenyButton: true,
            confirmButtonText: 'Yes',
            denyButtonText: `No`,
        }).then((result) => {
            if (!result.isConfirmed) return;

            $.ajax({
                type: "POST",
                url: `${window.location.origin}/Tickets/RemoveTicket`,
                data: { id: ticketId },
                dataType: "json",
                error: function (event, jqxhr, settings, thrownError) {
                    if (event.status != 200) {
                        Swal.fire(
                            'Error!',
                            event.responseText.split(': ')[1].split("\r\n")[0],
                            'error'
                        );
                    } else {
                        Swal.fire(
                            'Success!',
                            "Ticket removed successfully !",
                            'success'
                        ).then(() => window.location.reload());
                    }
                }
            });
        });
    });

$(document).on("click",
    '.admin-delete-ticket',
    function () {
        var ticketId = $(this).attr('data-id');

        Swal.fire({
            title: 'Do you want to delete the ticket ?',
            showDenyButton: true,
            confirmButtonText: 'Yes',
            denyButtonText: `No`,
        }).then((result) => {
            if (!result.isConfirmed) return;

            $.ajax({
                type: "POST",
                url: `${window.location.origin}/Tickets/AdminDeleteTicket`,
                data: { id: ticketId },
                dataType: "json",
                error: function (event, jqxhr, settings, thrownError) {
                    if (event.status != 200) {
                        Swal.fire(
                            'Error!',
                            event.responseText.split(': ')[1].split("\r\n")[0],
                            'error'
                        );
                    } else {
                        Swal.fire(
                            'Success!',
                            "Ticket deleted successfully !",
                            'success'
                        ).then(() => window.location.reload());
                    }
                }
            });
        });
    });