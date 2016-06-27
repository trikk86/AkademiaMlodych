var doses = new Array();

var user;


var userID = 1;

var meds = [];

function formatDate(date) {
    var hours = date.getUTCHours();
    var minutes = date.getMinutes();
    hours = hours < 10 ? '0' + hours : hours;
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes;
    return strTime;
}

function getUser() {
    $.ajax({
        url: 'http://localhost:50643/api/User/Details/' + userID,
        type: 'GET',
        dataType: 'json',
        success: function (userDetails) {
            user = userDetails;
            $('#infobox').prepend('<button style="float:right" type="button" onclick="location.href=\'login.html\';" class="btn btn-danger"><span class="glyphicon glyphicon-log-out"></span></button><p>Zalogowany jako ' + user.Name + " " + user.Surname + '</p>');
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function deleteMed(A) {
    $.ajax({
        url: 'http://localhost:50643/api/Medicine/Delete/' + A,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $("#list").empty();
            getDoses();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });

    $("#list").empty();
    getDoses();
}

function getDoses() {
    $("#list").empty();
    doses = [];
    $.ajax({
        url: 'http://localhost:50643/api/Medicine/GetMedicinesForUser/' + userID,
        type: 'GET',
        dataType: 'json',
        success: function (med) {
            

            for (var i = 0; i < med.length; i++) {
                var start = new Date(med[i].Beginning_Date);
                var end = new Date(med[i].The_End_Date);

                var dose = {
                    id: med[i].MedicineID,
                    drug_name: med[i].Medicine_Name,
                    dose: med[i].Amount,
                    what_time: formatDate(start),
                    start_day: ((start.getMonth() + 1) < 10 ? '0' + (start.getMonth() + 1) : (start.getMonth() + 1)) + "/" + (((start.getDate() + 1) < 10 ? '0' + (start.getDate() + 1) : (start.getDate() + 1)) - 1) + "/" + start.getFullYear(),
                    end_day: ((end.getMonth() + 1) < 10 ? '0' + (end.getMonth() + 1) : (end.getMonth() + 1)) + "/" + (((end.getDate() + 1) < 10 ? '0' + (end.getDate() + 1) : (end.getDate() + 1)) - 1) + "/" + end.getFullYear(),
                    how_long: med[i].Tolerance_Hour,
                    comment: med[i].Comment,
                    freq: med[i].FrequencyOptionId.toString(),
                    freq_opt: new String,
                    freq_opts: new Array(7)
                }

                if (dose.freq == "3") {
                    dose.freq_opt = med[i].FrequencyOptionValue;
                }

                var freqopt = med[i].FrequencyOptionValue;

                if (dose.freq == "4") {
                    for (var j = 1; j < 8; j++) {
                        if (freqopt.indexOf(i.toString()) > (-1)) {
                            dose.freq_opts[j - 1] = true;
                        }
                    }
                }

                doses.push(dose);
            }
            meds = med.slice();
            load_doses();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });

    load_doses();

    return;
}

function addDose(newdose) {
    var med = {
        UserID: userID,
        Medicine_Name: newdose.drug_name,
        Amount: newdose.dose,
        Comment: newdose.comment,
        Beginning_Date: newdose.start_day + " " + newdose.what_time,
        The_End_Date: newdose.end_day + " " + newdose.what_time,
        Tolerance_Hour: parseInt(newdose.how_long),
        FrequencyOptionId: parseInt(newdose.freq),
    }
    if (med.FrequencyOptionId == 3) {
        med.FrequencyOptionValue = newdose.freq_opt;
    }
    if (med.FrequencyOptionId == 4) {
        med.FrequencyOptionValue = "";

        for (var i = 0 ; i < 7; i++) {
            if (newdose.freq_opts[i])
                med.FrequencyOptionValue += (i + 1).toString() + ",";
        }
    }

    $.ajax({
        url: 'http://localhost:50643/api/Medicine/Create',
        type: 'POST',
        dataType: 'json',
        data: med,
        success: function (id) {
            getDoses();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
    return true;
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
    }

    return false;
}

function load_doses() {
    $("#list").empty();

    var freq = "";

    for (var i = 0; i < doses.length; i++) {

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
    '<a data-toggle="collapse" href="#detail' + doses[i].id + '" class="list-group-item" id="' + doses[i].id + '">' +
                '<h4>' + doses[i].drug_name + '</h4>' +
                '<div id="detail' + doses[i].id + '" class="collapse">' +
                    '<p><b>Godzina: </b>' + doses[i].what_time + '</p>' +
                    '<p><b>Dawka: </b>' + doses[i].dose + '</p>' +
                    '<p><b>Częstotliwość: </b>' + freq + '</p>' +
                    '<span data-toggle="modal" data-target="#form"><button data-placement="bottom" data-toggle="tooltip" href="javascript://" title="Edytuj" type="button" class="btn btn-primary" onclick="edit(' + doses[i].id + ')"><span class="glyphicon glyphicon-pencil"></span></button></span>' + " " +
                    '<span  data-toggle="modal" data-target="#form"><button type="button" data-placement="bottom" data-toggle="tooltip" href="javascript://" title="Duplikuj" class="btn btn-success" onclick="$(\'#save\').hide(); $(\'#submit\').show(); duplicate(' + doses[i].id + ')"><span class="glyphicon glyphicon-duplicate"></span></button></span>' + " " +
                    '<button type="button" data-toggle="tooltip" data-placement="bottom" title="Usuń" class="btn btn-danger" onclick="deleteMed(' + doses[i].id + ')"><span class="glyphicon glyphicon-trash"></span></button>' + " " +
                '</div>' +
            '</a>'
         );
}

    loadCalendar();
}

$(window).load(function () {
    getUser();

    getDoses();
    
    $("#save").hide();
});



function add() {

    event.preventDefault();

    $('.alert').hide();

    var form = document.forms[0];

    var freq1 = document.getElementsByName("freq");
    
    var cos;

    for (var i = 0; i < freq1.length; i++) {
        if (freq1[i].checked == true) {
            cos = freq1[i].value;
        }
    }

    var newDose = {
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
        addDose(newDose);
    }

    $('#form').modal('hide');

    $('.alert').hide();
    form.reset();
    document.getElementsByName("freq")[0].checked = true;
}

function duplicate(id) {
    
    $(document).ready();

    $('.alert').hide();

    document.forms[0].reset();

    var i = 0;

    while (doses[i].id != id) {
        i++;
    }

    var dose = doses[i];

    if (dose.drug_name == "" || dose.dose == "" || dose.what_time == "" || dose.how_long == "" || dose.start_day == "" || (dose.freq == 3 && dose.freq_opts == "") || (dose.freq == 4 && (!dose.freq_opts[0] && !dose.freq_opts[1] && !dose.freq_opts[2] && !dose.freq_opts[3] && !dose.freq_opts[4] && !dose.freq_opts[5] && !dose.freq_opts[6]))) {
        $('.alert').show();
        return;
    }
    
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

    $('.alert').hide();

    document.forms[0].reset();

    var i = 0;

    while (doses[i].id != id) {
        i++;
    }

    var dose = doses[i];

    if (dose.drug_name == "" || dose.dose == "" || dose.what_time == "" || dose.how_long == "" || dose.start_day == "" || (dose.freq == 3 && dose.freq_opts == "") || (dose.freq == 4 && (!dose.freq_opts[0] && !dose.freq_opts[1] && !dose.freq_opts[2] && !dose.freq_opts[3] && !dose.freq_opts[4] && !dose.freq_opts[5] && !dose.freq_opts[6]))) {
        $('.alert').show();
        return;
    }

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

    if (newDose.drug_name == "" || newDose.dose == "" || newDose.what_time == "" || newDose.how_long == "" || newDose.start_day == "" || (newDose.freq == 3 && newDose.freq_opts == "") || (newDose.freq == 4 && (!newDose.freq_opts[0] && !newDose.freq_opts[1] && !newDose.freq_opts[2] && !newDose.freq_opts[3] && !newDose.freq_opts[4] && !newDose.freq_opts[5] && !newDose.freq_opts[6]))) {
        $('.alert').show();
        return;
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

        for (var j = 0; j < meds.length; j++) {

            for (var k = 0; k < meds[j].Doses.length; k++) {

                var dose = meds[j].Doses[k];

                var actDate = new Date(dose.Date);

                if (newdate.getFullYear() == actDate.getFullYear() && newdate.getMonth() == actDate.getMonth() && newdate.getDate() == actDate.getDate()) {
                    popover += formatDate(actDate) + ' ' + meds[j].Medicine_Name + '<br/>';
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

        for (var j = 0; j < meds.length; j++) {

            for (var k = 0; k < meds[j].Doses.length; k++) {

                var dose = meds[j].Doses[k];

                var actDate = new Date(dose.Date);

                if (newdate.getFullYear() == actDate.getFullYear() && newdate.getMonth() == actDate.getMonth() && newdate.getDate() == actDate.getDate()) {
                    popover += formatDate(actDate) + ' ' + meds[j].Medicine_Name + " ";

                    if (meds[j].Doses[k].ifTaken) {
                        popover += "<span class='glyphicon glyphicon-ok'></span>" + "</br>";
                    }
                    else {
                        popover += "<span class='glyphicon glyphicon-remove'></span>" + "</br>";
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


