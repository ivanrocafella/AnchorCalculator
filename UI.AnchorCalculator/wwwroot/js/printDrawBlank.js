function PrintDraw(id) {
    $.ajax(
        {            
            url: '/anchor/Anchor/GetAnchorJsonResult',
            type: 'GET',
            data: { 'id': id },
            success: function (response) {
                if (response.success) {
                    console.log(response.anchor, 'production ' + response.anchor.productionId + '')
                    var firstName = 'Шпилька';  
                    var form;                             
                    var executor = response.anchor.user.userName;
                    var billetLength = Math.round(response.anchor.billetLength);
                    var bendRadius = response.anchor.bendRadius;
                    var quantity = response.anchor.quantity;
                    var threadDiameter = response.anchor.threadDiameter;
                    var length = response.anchor.length;
                    var threadLength = response.anchor.threadLength;
                    var materialFullName = response.anchor.material.fullName;
                    var withoutBindThreadDiamMatetial = response.anchor.withoutBindThreadDiamMatetial;
                    var dateNow = new Date();
                    var date = dateNow.getDate().toString().padStart(2, '0');
                    var month = (dateNow.getMonth() + 1).toString().padStart(2, '0');
                    var year = dateNow.getFullYear().toString();
                    var lengthBeforeEndPathRoller = Math.round(response.anchor.lengthBeforeEndPathRoller);
                    var productionStr = '';
                    var withoutBindThreadDiamMatetialInfo = '';
                    if (response.anchor.productionId === 0) {
                        productionStr = 'Изготовить на токарном станке';
                        console.log('production ' + response.anchor.productionId + '');
                    } else if (response.anchor.productionId === 1) {
                        productionStr = 'Изготовить на резьбонакатном станке';
                    } else {
                        productionStr = 'Изготовить на гидравлическом станке';
                    }
                    console.log(date);
                    console.log(month);
                    console.log(year);
                    var dateNowFormat = date + '.' + month + '.' + year;

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
                    var toleranceNum = 3;
                    var productionNum = 5;
                    var lengthPathRollerEnd = '<p class="card-text" > 2. Длина до конца пути ролика ' + lengthBeforeEndPathRoller + ' мм</p>';          
                    var tooling = '<span class="mr-auto">4. Оснастка: центр: палец ' + fingerCenter + ', оправка: ' + mandrelCenter + '; бок: палец ' + fingerSide + ', оправка ' + mandrelSide + '';
                    if (response.anchor.kind == 0) {
                        toleranceNum = 2;
                        productionNum = 3;
                        lengthPathRollerEnd = '';
                        tooling = '';
                        if (response.anchor.threadLengthSecond > 0) {
                            form = 'прямая две резьбы';
                        } else {
                            form = 'прямая';
                        }
                    } else if (response.anchor.kind == 1) {
                        form = 'гнутая';
                    } else {
                        firstName = 'Хомут';
                        form = 'прямоугольный';
                    }
                    if (withoutBindThreadDiamMatetial) {
                        console.log('withoutBindThreadDiamMatetialInfo - ' + withoutBindThreadDiamMatetialInfo + '');
                        withoutBindThreadDiamMatetialInfo = '<p class="card-text">' + (productionNum + 1) + '. Проточить до диаметра резьбы на токарном станке</p>';
                    }
                    var notes;
                    if (threadLength > 0) {
                            notes =
                            '<div><p class="card-text fw-bold">Кол - во: ' + quantity + ' шт.</p>' +
                            '<p class="card-text">1. Размер заготовки ' + billetLength + ' мм</p>' +
                            lengthPathRollerEnd +
                            '<p class="card-text">' + toleranceNum + '. Поле допуска на диаметр резьбы 8q по ГОСТ 16093</p>' +
                            '<p class="card-text"' +
                            tooling + '</p>' +
                            '<p class="card-text" > ' + productionNum + '. ' + productionStr + '</p>' + withoutBindThreadDiamMatetialInfo +'</div > ';
                    } else {
                            notes =
                            '<div><p class="card-text fw-bold">Кол - во: ' + quantity + ' шт.</p>' +
                            '<p class="card-text">1. Размер заготовки ' + billetLength + ' мм</p>' +
                            lengthPathRollerEnd +
                            '<p class="card-text">' + toleranceNum + '. Поле допуска на диаметр резьбы 8q по ГОСТ 16093</p>' +
                            '<p class="card-text"' +
                            tooling + '</p></div > ';
                    }
                    

                    var stamp = '<table class="table table-bordered border-dark">' +
                        '<thead>' +
                        '<tr>' +
                        '<th colspan="2" class="text-center">' + firstName + ' M' + threadDiameter + 'x' + length + ' ' + form + '</th>' +
                        '<td class="text-left">Тип заказа:</td>' +
                        '<td class="text-center">' + materialFullName + '</td>' +
                        '</tr>' +
                        '</thead>' +
                        '<tbody>' +
                        '<tr>' +    
                        '<th scope="col" class="w-25">Исполнитель</th>' +
                        '<td class="w-30">' + executor + '</td>' +
                        '<td class="w-25"></td>' +
                        '<td class="text-center w-20">' + dateNowFormat + '</td>' +
                        '</tr>' +
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