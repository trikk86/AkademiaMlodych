var doses = [
    {
        drug_name: "Witamina C",
        dose: "2 tabletki",
        what_time: "7:00",
        how_long: 3,
        start_day: "13.05.2016",
        end_day: "",
        comment: "",
        freq: "everyday",
        freq_opt: 0,
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

var counter = 1;

function load_doses() {
    for (i = 0; i < doses.length; i++) {
        $("#list").prepend(
    '<a data-toggle="collapse" href="#detail' + i + '" class="list-group-item list-group-item-success">' +
                '<h4>' + doses[i].drug_name + '</h4>' +
                '<div id="detail' + i + '" class="collapse">' +
                    '<p><b>Godzina: </b>' + doses[i].what_time + '</p>' +
                    '<p><b>Dawka: </b>' + doses[i].dose + '</p>' +
                    '<p><b>Częstotliwość: </b>' + doses[i].freq + '</p>' +
                '</div>' +
            '</a>'
         );
    }
}

$(window).load(function () {
    load_doses();
});

load_doses();

function add() {

    event.preventDefault();

    var form = document.forms[0];

    var newDose = {
        drug_name: form.drug_name.value,
        dose: form.dose.value,
        what_time: form.what_time.value,
        how_long: form.how_long.value,
        start_day: form.start_day.value,
        end_day: form.end_day.value,
        comment: form.comment.value,
        freq: form.freq.value,
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

    counter = counter + 1;

    doses.push(newDose);

    

    $("#list").prepend(
    '<a data-toggle="collapse" href="#detail' + counter + '" class="list-group-item list-group-item-success">' +
                '<h4>' + newDose.drug_name + '</h4>' +
                '<div id="detail' + counter + '" class="collapse">' +
                    '<p><b>Godzina: </b>' + newDose.what_time + '</p>' +
                    '<p><b>Dawka: </b>' + newDose.dose + '</p>' +
                    '<p><b>Częstotliwość: </b>' + newDose.freq + '</p>' +
                '</div>' +
            '</a>'
    );

    

    $('#form').modal('hide');

    form.reset();
}


