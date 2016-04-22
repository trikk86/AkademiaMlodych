var doses = [
    {
        drug_name: "Witamina C",
        dose: "2 tabletki",
        what_time: "7:00",
        how_long: 3,
        start_day: "13.05.2016",
        end_day: "",
        comment: "",
        freq: 1,
        freq_opt: 0,
        freq_opts[
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

    function add() {
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

        doses.push(newDose);
    }

