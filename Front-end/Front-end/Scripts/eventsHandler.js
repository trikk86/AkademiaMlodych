var doses = [
    
]

var days_of_week = [
    "Poniedziałek",
    "Wtorek",
    "Środa",
    "Czwartek",
    "Piątek",
    "Sobota",
    "Niedziela"
]

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
    '<a data-toggle="collapse" href="#detail' + i + '" class="list-group-item list-group-item-success" id="' + i + '">' +
                '<h4>' + doses[i].drug_name + '</h4>' +
                '<div id="detail' + i + '" class="collapse">' +
                    '<p><b>Godzina: </b>' + doses[i].what_time + '</p>' +
                    '<p><b>Dawka: </b>' + doses[i].dose + '</p>' +
                    '<p><b>Częstotliwość: </b>' + freq + '</p>' +
                    '<button type="button" class="btn btn-danger" onclick="$(\'#' + i + '\').remove(); doses.splice(' + i + ', 1);">Usuń</button>' +
                    '<button data-toggle="modal" data-target="#form" type="button" class="btn" onclick="duplicate(' + i + ')">Duplikuj</button>' +
                '</div>' +
            '</a>'
         );
    }
}

$(window).load(function () {
    load_doses();
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

    var alreadyOn = false;

    for(var i = 0; i < doses.length; i++) {
        if (JSON.stringify(newDose) === JSON.stringify(doses[i])) {
            alreadyOn = true;
        }
    }

    if (!alreadyOn) {
        doses.push(newDose);

        load_doses();
    }

    $('#form').modal('hide');

    form.reset();
}

function duplicate(i) {
    $(document).ready();

    var dose = doses[i];
    
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


