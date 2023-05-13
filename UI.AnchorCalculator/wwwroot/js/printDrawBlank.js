function PrintDraw(id) {
    $.ajax(
        {
            url: '/Anchor/GetAnchorJsonResult',
            type: 'GET',
            data: { 'id': id },
            success: function (response) {
                if (response.success) {
                    console.log(response.anchor)
                    var executor = response.anchor.user.userName;
                    var billetLength = Math.round(response.anchor.billetLength);
                    var bendRadius = response.anchor.bendRadius;
                    var quantity = response.anchor.quantity;
                    var threadDiameter = response.anchor.threadDiameter;
                    var length = response.anchor.length;
                    var materialFullName = response.anchor.material.fullName;
                    var dateNow = new Date();
                    var dateNowFormat = ((dateNow.getMonth() > 8) ? (dateNow.getMonth() + 1) : ('0' + (dateNow.getMonth() + 1))) + '.' + ((dateNow.getDate() > 9) ? dateNow.getDate() : ('0' + dateNow.getDate())) + '.' + dateNow.getFullYear()

                    console.log(bendRadius)
                    var fingerCenter = "";
                    var mandrelCenter = "";
                    var fingerSide = "";
                    var mandrelSide = "";
                    if (bendRadius > 0 && bendRadius <= 12) {
                        fingerCenter = "1";
                        mandrelCenter = "_";
                        fingerSide = "7";
                        mandrelSide = "O11";
                    } else if (bendRadius > 12 && bendRadius <= 16) {
                        fingerCenter = "3";
                        mandrelCenter = "_";
                        fingerSide = "7";
                        mandrelSide = "O10";
                    } else if (bendRadius > 16 && bendRadius <= 20) {
                        fingerCenter = "4";
                        mandrelCenter = "_";
                        fingerSide = "7";
                        mandrelSide = "O9";
                    } else if (bendRadius > 20 && bendRadius <= 24) {
                        fingerCenter = "6";
                        mandrelCenter = "_";
                        fingerSide = "7";
                        mandrelSide = "O6";
                    } else if (bendRadius > 24 && bendRadius <= 30) {
                        fingerCenter = "8";
                        mandrelCenter = "_";
                        fingerSide = "7";
                        mandrelSide = "O1";
                    } else if (bendRadius > 30 && bendRadius <= 36) {
                        fingerCenter = "7";
                        mandrelCenter = "O4";
                        fingerSide = "4";
                        mandrelSide = "_";
                    } else if (bendRadius > 36 && bendRadius <= 42) {
                        fingerCenter = "7";
                        mandrelCenter = "O6";
                        fingerSide = "7";
                        mandrelSide = "O8";
                    } else if (bendRadius > 42 && bendRadius <= 48) {
                        fingerCenter = "7";
                        mandrelCenter = "O8";
                        fingerSide = "7";
                        mandrelSide = "O3";
                    } else {
                        fingerCenter = "_";
                        mandrelCenter = "_";
                        fingerSide = "_";
                        mandrelSide = "_";
                    }

                    var notes =
                        '<div><p class="card-text">1. *Размер для справок</p>' +
                        '<p class="card-text">2. Размер заготовки ' + billetLength + ' мм</p>' +
                        '<p class="card-text">3. Поле допуска на диаметр резьбы 8q по ГОСТ 16093</p>' +
                        '<p class="card-text d-flex justify-content-between">' +
                        '<span class="mr-auto">4. Оснастка: центр: палец ' + fingerCenter + ', оправка: ' + mandrelCenter + '; бок: палец ' + fingerSide + ', оправка ' + mandrelSide + '' +
                        '</span><span style="text-align: right;">Кол - во: ' + quantity + ' шт.</span></p></div>';

                    var stamp = '<table class="table table-bordered border-dark">' +
                        '<thead>' +
                        '<tr>' +
                        '<th colspan="3" class="text-center">Анкер M' + threadDiameter + 'x' + length + '</th>' +
                        '<td class="text-center">' + materialFullName + '</th>' +
                        '</tr>' +
                        '</thead>' +
                        '<tbody>' +
                        '<tr>' +
                        '<th scope="col" class="w-25">Исполнитель</th>' +
                        '<td class="w-30">' + executor + '</td>' +
                        '<td class="w-25"></td>' +
                        '<td class="text-center w-20">' + dateNowFormat + '</td>' +
                        '</tr>' +
                        '<tr>' +
                        '<th scope="col" class="w-25">Гл. инженер</th>' +
                        '<td class="w-30">Никуленко</td>' +
                        '<td class="w-25"></td>' +
                        '<td class="text-center w-20">' + dateNowFormat + '</td>' +
                        '</tr>' +
                        '<tr>' +
                        '</tbody>' +
                        '</table>';

                    console.log(true);
                    var drawing_pdf = document.getElementById("drawing_pdf");
                    var notes_pdf = document.getElementById("notes_pdf");
                    notes_pdf.innerHTML = notes;
                    var stamp_pdf = document.getElementById("stamp_pdf");
                    stamp_pdf.innerHTML = stamp;



                    console.log(drawing_pdf);
                    drawing_pdf.style.display = "block";
                    $('#drawing_pdf').printThis();
                    setTimeout(() => {
                        drawing_pdf.style.display = "none";
                    }, 500)
                }
            }
        })

}


function PrintBlank() {
    var blank_pdf = document.getElementById("blank_pdf");
    blank_pdf.style.display = "block";
    $('#blank_pdf').printThis();
    setTimeout(() => {
        blank_pdf.style.display = "none";
    }, 500)
}