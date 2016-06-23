var doses = [
    {
        id: "0",
        drug_name: "Witamina C",
        dose: "2 tabletki",
        what_time: "07:00",
        how_long: "10",
        start_day: "03/20/2016",
        end_day: "",
        comment: "",
        freq: "1",
        freq_opt: "",
        freq_opts: [
            false,
            false,
            false,
            false,
            false,
            false,
            false
        ]
    }
]

var user = {
    ID: "0",
    username: "vangradt",
}

var counter = 4;

var userID = 0;

var med = new [];

function getDoses() {
    var theUrl = "localhost:1234/" + userID;

    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, false);
    xmlHttp.send(null);
    med = JSON.parse(xmlHttp.responseText);

    for (var i = 0; i < med.size() ; i++) {
        var dose = {
            id: med[i].medId,
            drug_name: med[i].medName,
            dose: med[i].amount,
            what_time: "07:00",
            how_long: "10",
            start_day: "03/20/2016",
            end_day: "",
            comment: "",
            freq: "1",
            freq_opt: "",
            freq_opts: [
                false,
                false,
                false,
                false,
                false,
                false,
                false
            ]
        }
    }

    return doses;
}

function addDose(newdose) {
    JSON.stringify(newdose);

    ifSuccess = true;

    //TODO

    return ifSuccess;
}

var days_of_week = [
    "Poniedziałek",
    "Wtorek",
    "Środa",
    "Czwartek",
    "Piątek",
    "Sobota",
    "Niedziela"
]

$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
    $('[data-toggle="tooltip"]').tooltip();
});

function compareDoses(dose1, dose2) {
    if(dose1.drug_name == dose2.drug_name &&
        dose1.dose == dose2.dose &&
        dose1.what_time == dose2.what_time &&
        dose1.how_long == dose2.how_long &&
        dose1.start_day == dose2.start_day &&
        dose1.end_day == dose2.end_day &&
        dose1.comment == dose2.comment &&
        dose1.freq == dose2.freq &&
        dose1.freq_opt == dose2.freq_opt &&
        dose1.freq_opts[0] == dose2.freq_opts[0] &&
        dose1.freq_opts[1] == dose2.freq_opts[1] &&
        dose1.freq_opts[2] == dose2.freq_opts[2] &&
        dose1.freq_opts[3] == dose2.freq_opts[3] &&
        dose1.freq_opts[4] == dose2.freq_opts[4] &&
        dose1.freq_opts[5] == dose2.freq_opts[5] &&
        dose1.freq_opts[6] == dose2.freq_opts[6]
        ) {
        return true;
    };

    return false;
}

function load_doses() {
    $("#list").empty();

    var freq = "";

    for (i = 0; i < doses.length; i++) {

        switch (doses[i].freq) {
            case "1":
                freq = "Codziennie";
                break;
            case "2":
                freq = "Co tydzień";
                break;
            case "3":
                freq = "Co " + doses[i].freq_opt + " dni";
                break;
            case "4":
                freq = "";
                for (var j = 0; j < 7; j++) {
                    if (doses[i].freq_opts[j] == true)
                        freq = freq + " " + days_of_week[j];
                }
                break;
        }

        $("#list").prepend(
    '<a data-toggle="collapse" href="#detail' + i + '" class="list-group-item" id="' + i + '">' +
                '<h4>' + doses[i].drug_name + '</h4>' +
                '<div id="detail' + i + '" class="collapse">' +
                    '<p><b>Godzina: </b>' + doses[i].what_time + '</p>' +
                    '<p><b>Dawka: </b>' + doses[i].dose + '</p>' +
                    '<p><b>Częstotliwość: </b>' + freq + '</p>' +
                    '<span data-toggle="modal" data-target="#form"><button data-placement="bottom" data-toggle="tooltip" href="javascript://" title="Edytuj" type="button" class="btn btn-primary" onclick="edit(' + doses[i].id + ')"><span class="glyphicon glyphicon-pencil"></span></button></span>' + " " +
                    '<span  data-toggle="modal" data-target="#form"><button type="button" data-placement="bottom" data-toggle="tooltip" href="javascript://" title="Duplikuj" class="btn btn-success" onclick="duplicate(' + doses[i].id + ')"><span class="glyphicon glyphicon-duplicate"></span></button></span>' + " " +
                    '<button type="button" data-toggle="tooltip" data-placement="bottom" title="Usuń" class="btn btn-danger" onclick="$(\'#' + i + '\').remove(); doses.splice(' + i + ', 1); load_doses();"><span class="glyphicon glyphicon-trash"></span></button>' + " " +
                '</div>' +
            '</a>'
         );
    }

    loadCalendar();
}

$(window).load(function () {
    $('#infobox').prepend('<button style="float:right" type="button" onclick="location.href=\'login.html\';" class="btn btn-danger"><span class="glyphicon glyphicon-log-out"></span></button><p>Zalogowany jako ' + user.username + '</p>');

    doses = getDoses();
    $('.alert').hide();

    load_doses();
    $("#save").hide();
});



function add() {

    event.preventDefault();

    var form = document.forms[0];

    var freq1 = document.getElementsByName("freq");
    
    var cos;

    for (var i = 0; i < freq1.length; i++) {
        if (freq1[i].checked == true) {
            cos = freq1[i].value;
        }
    }

    var newDose = {
        id: counter,
        drug_name: form.drug_name.value,
        dose: form.dose.value,
        what_time: form.what_time.value,
        how_long: form.how_long.value,
        start_day: form.start_day.value,
        end_day: form.end_day.value,
        comment: form.comment.value,
        freq: cos,
        freq_opt: form.freq_opt.value,
        freq_opts: [
            form.freq_opt1.checked,
            form.freq_opt2.checked,
            form.freq_opt3.checked,
            form.freq_opt4.checked,
            form.freq_opt5.checked,
            form.freq_opt6.checked,
            form.freq_opt7.checked,
        ]
    }

    if (newDose.drug_name == "" || newDose.dose == "" || newDose.what_time == "" || newDose.how_long == "" || newDose.start_day == "" || (newDose.freq == 3 && newDose.freq_opts == "") || (newDose.freq == 4 && (!newDose.freq_opts[0] && !newDose.freq_opts[1] && !newDose.freq_opts[2] && !newDose.freq_opts[3] && !newDose.freq_opts[4] && !newDose.freq_opts[5] && !newDose.freq_opts[6]))) {
        $('.alert').show();
        return;
    }

    var alreadyOn = false;

    for(var i = 0; i < doses.length; i++) {
        if (compareDoses(newDose, doses[i])) {
            alreadyOn = true;
        }
    }

    if (!alreadyOn) {
        if (addDose(newDose)) {
            doses.push(newDose);

            counter++;

            load_doses();
        }
        else {
            alert("Błąd!");
        }
    }

    $('#form').modal('hide');

    $('.alert').hide();
    form.reset();
    document.getElementsByName("freq")[0].checked = true;
}

function duplicate(id) {
    $(document).ready();

    var i = 0;

    while (doses[i].id != id) {
        i++;
    }

    var dose = doses[i];
    
    document.forms[0].id.value = dose.id;
    document.forms[0].drug_name.value = dose.drug_name;
    document.forms[0].dose.value = dose.dose;
    document.forms[0].what_time.value = dose.what_time;
    document.forms[0].how_long.value = dose.how_long;
    document.forms[0].start_day.value = dose.start_day;
    document.forms[0].end_day.value = dose.end_day;
    document.forms[0].comment.value = dose.comment;

    switch (dose.freq) {
        case "1":
            document.getElementsByName("freq")[0].checked = true;
            break;
        case "2":
            document.getElementsByName("freq")[1].checked = true;
            break;
        case "3":
            document.getElementsByName("freq")[2].checked = true;
            break;
        case "4":
            document.getElementsByName("freq")[3].checked = true;
            break;
    }

    document.forms[0].freq_opt.value = dose.freq_opt;

    $(document).ready();

    document.forms[0].freq_opt1.checked = dose.freq_opts[0];
    document.forms[0].freq_opt2.checked = dose.freq_opts[1];
    document.forms[0].freq_opt3.checked = dose.freq_opts[2];
    document.forms[0].freq_opt4.checked = dose.freq_opts[3];
    document.forms[0].freq_opt5.checked = dose.freq_opts[4];
    document.forms[0].freq_opt6.checked = dose.freq_opts[5];
    document.forms[0].freq_opt7.checked = dose.freq_opts[6];
}

function edit(id) {
    $(document).ready();

    var i = 0;

    while (doses[i].id != id) {
        i++;
    }

    var dose = doses[i];

    document.forms[0].id.value = dose.id;
    document.forms[0].drug_name.value = dose.drug_name;
    document.forms[0].dose.value = dose.dose;
    document.forms[0].what_time.value = dose.what_time;
    document.forms[0].how_long.value = dose.how_long;
    document.forms[0].start_day.value = dose.start_day;
    document.forms[0].end_day.value = dose.end_day;
    document.forms[0].comment.value = dose.comment;

    switch (dose.freq) {
        case "1":
            document.getElementsByName("freq")[0].checked = true;
            break;
        case "2":
            document.getElementsByName("freq")[1].checked = true;
            break;
        case "3":
            document.getElementsByName("freq")[2].checked = true;
            break;
        case "4":
            document.getElementsByName("freq")[3].checked = true;
            break;
    }

    document.forms[0].freq_opt.value = dose.freq_opt;

    $(document).ready();

    document.forms[0].freq_opt1.checked = dose.freq_opts[0];
    document.forms[0].freq_opt2.checked = dose.freq_opts[1];
    document.forms[0].freq_opt3.checked = dose.freq_opts[2];
    document.forms[0].freq_opt4.checked = dose.freq_opts[3];
    document.forms[0].freq_opt5.checked = dose.freq_opts[4];
    document.forms[0].freq_opt6.checked = dose.freq_opts[5];
    document.forms[0].freq_opt7.checked = dose.freq_opts[6];

    $("#submit").hide();
    $("#save").show();
}

function saveDose() {

    event.preventDefault();

    var form = document.forms[0];

    var freq1 = document.getElementsByName("freq");

    var cos;

    for (var i = 0; i < freq1.length; i++) {
        if (freq1[i].checked == true) {
            cos = freq1[i].value;
        }
    }

    var i = 0;

    while(doses[i].id != form.id.value) {
        i++;
    }

    var newDose = {
        id: form.id.value,
        drug_name: form.drug_name.value,
        dose: form.dose.value,
        what_time: form.what_time.value,
        how_long: form.how_long.value,
        start_day: form.start_day.value,
        end_day: form.end_day.value,
        comment: form.comment.value,
        freq: cos,
        freq_opt: form.freq_opt.value,
        freq_opts: [
            form.freq_opt1.checked,
            form.freq_opt2.checked,
            form.freq_opt3.checked,
            form.freq_opt4.checked,
            form.freq_opt5.checked,
            form.freq_opt6.checked,
            form.freq_opt7.checked,
        ]
    }

    doses.splice(i, 1);

    var alreadyOn = false;

    for (var i = 0; i < doses.length; i++) {
        if (JSON.stringify(newDose) === JSON.stringify(doses[i])) {
            alreadyOn = true;
        }
    }

    if (!alreadyOn) {
        doses.push(newDose);

        load_doses();
    }
    
    $('#save').hide();
    $('#submit').show();
    $('#form').modal('hide');

    form.reset();
}

function loadCalendar() {


    var today = new Date();

    for (var i = 7; i > 0; i--) {
        

        var date = new Date();

        date.setDate(date.getDate() + i);

        i += 7;

        var newdate = new Date(date);

        var dd = newdate.getDate();
        var mm = newdate.getMonth() + 1; //January is 0!
        var yyyy = newdate.getFullYear();

        if (dd < 10) {
            dd = '0' + dd
        }

        if (mm < 10) {
            mm = '0' + mm
        }

        today = dd + '/' + mm + '/' + yyyy;

        var popover = "";

        for (var j = 0; j < doses.length; j++) {
            var dose = doses[j];

            var startDate = new Date(dose.start_day);

            if (dose.end_day != "") {
                var endDate = new Date(dose.end_day);



                if (newdate.getTime() >= startDate.getTime() && newdate.getTime() <= endDate.getTime()) {
                    if (dose.freq == 1) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';

                    }

                    var diffTime = newdate.getTime() - startDate.getTime();

                    var diffDate = new Date();

                    diffDate.setTime(diffTime);

                    var diffDays = diffDate.getDate();

                    if (dose.freq == 2 && diffDays % 7 == 1) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';

                    }

                    if (dose.freq == 3 && diffDays % dose.freq_opt == 1) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';

                    }

                    if (dose.freq == 4 && dose.freq_opts[(newdate.getDay() + 6) % 7]) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';

                    }
                }
            }
            else {
                if (newdate.getTime() >= startDate.getTime()) {
                    if (dose.freq == 1) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';


                    }

                    var diffTime = newdate.getTime() - startDate.getTime();

                    var diffDate = new Date();

                    diffDate.setTime(diffTime);

                    var diffDays = diffDate.getDate();

                    if (dose.freq == 2 && diffDays % 7 == 1) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';

                    }

                    if (dose.freq == 3 && diffDays % dose.freq_opt == 1) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';


                    }

                    if (dose.freq == 4 && dose.freq_opts[(newdate.getDay() + 6) % 7]) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';

                    }
                }
            }
        }

        i -= 7;

        var nameOfDay = "#day" + (i + 7);

        $(nameOfDay).empty();

        $(nameOfDay).attr('data-content', popover);
        $(nameOfDay).append(days_of_week[(newdate.getDay() + 6) % 7] + '</p>' + '<p>' + today + '</p>');

        
    }

    for (var i = 0; i < 7; i++) {
        var date = new Date();

        date.setDate(date.getDate() - i);

        date.setHours(0, 0, 0, 0);

        var newdate = new Date(date);

        var dd = newdate.getDate();
        var mm = newdate.getMonth() + 1; //January is 0!
        var yyyy = newdate.getFullYear();

        if (dd < 10) {
            dd = '0' + dd
        }

        if (mm < 10) {
            mm = '0' + mm
        }

        today = dd + '/' + mm + '/' + yyyy;

        var popover = "";

        for (var j = 0; j < doses.length; j++) {
            var dose = doses[j];

            var startDate = new Date(dose.start_day);

            if (dose.end_day != "") {
                var endDate = new Date(dose.end_day);



                if (newdate.getTime() >= startDate.getTime() && newdate.getTime() <= endDate.getTime()) {
                    if (dose.freq == 1) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';


                    }

                    var diffTime = newdate.getTime() - startDate.getTime();

                    var diffDate = new Date();

                    diffDate.setTime(diffTime);

                    var diffDays = diffDate.getDate();

                    if (dose.freq == 2 && diffDays % 7 == 1) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';

                    }

                    if (dose.freq == 3 && diffDays % dose.freq_opt == 1) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';


                    }

                    if (dose.freq == 4 && dose.freq_opts[(newdate.getDay() + 6) % 7]) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';

                    }
                }
            }
            else {
                if (newdate.getTime() >= startDate.getTime()) {
                    if (dose.freq == 1) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';


                    }

                    var diffTime = newdate.getTime() - startDate.getTime();

                    var diffDate = new Date();

                    diffDate.setTime(diffTime);

                    var diffDays = diffDate.getDate();

                    if (dose.freq == 2 && diffDays % 7 == 1) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';

                    }

                    if (dose.freq == 3 && diffDays % dose.freq_opt == 1) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';


                    }

                    if (dose.freq == 4 && dose.freq_opts[(newdate.getDay() + 6) % 7]) {
                        popover += dose.what_time + ' ' + dose.drug_name + '<br/>';

                    }
                }
            }
        }

        var ifactive = "";

        if (i == 0) {
            ifactive = "today";
        }

        var nameOfDay = "#day" + (7 - i);

        $(nameOfDay).empty();

        $(nameOfDay).attr('data-content', popover);
        $(nameOfDay).addClass(ifactive);
        $(nameOfDay).append(days_of_week[(newdate.getDay() + 6) % 7] + '</p>' + '<p>' + today + '</p>');
    }

    $('.popover-btn').popover();
    $('[data-toggle="tooltip"]').tooltip();
}


