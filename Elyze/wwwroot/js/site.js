$(document).ready(function () {
    $('.menu__icon').click(function () {
        $('body').toggleClass('menu_shown');
    });
});

function redirect_to(id, action, section, base_url) {
    let ajaz = $.ajax({
        url: `${base_url}/${section}/${action}/${id}`,
        type: "GET",
        dataType: "html",
        success: function (data) {
            window.location.href = `${base_url}/${section}/${action}/${id}`;
        },
        error: function (xhr, status, error) {
            alert("Error: " + error);
        }
    });
}

function ConvertStringFromCamelToTitleHeader(str) {
    // Dividiamo la stringa in parole separate da caratteri maiuscoli
    const words = str.split(/(?=[A-Z])/);
    console.log("words", words)
    // Verifichiamo se ci sono parole separate solo dalle maiuscole iniziali
    const splitWords = words.flatMap(word => word.split(/(?=[A-Z][a-z])/));


    // Convertiamo ogni parola in minuscolo tranne la prima lettera
    const result = splitWords.map(word => {
        const firstLetter = word.slice(0, 1).toLowerCase();
        const restOfWord = word.slice(1).toLowerCase();
        return `${firstLetter}${restOfWord}`;
    });

    result[0] = result[0].charAt(0).toUpperCase() + result[0].slice(1).toLowerCase();

    // Uniamo le parole con uno spazio
    console.log("result.join(' ')", result.join(' '))
    return result.join(' ');
}

function reload_datatable(id = "#table_aree") {
    var table = $(id).DataTable();
    table.ajax.reload();
}

function LoadSede() {
    var strSelected = "";
    $("#SocietaId option:selected").each(function () {
        strSelected += $(this)[0].value;
    });
    $.ajax({
        url: "/Gen/Sedi/",
        method: "GET",
        type: "json",
        contentType: "application/json;charset=utf-8",
        data: { soc: strSelected }
    }).done((result) => {
        $("#StabilimentoId").empty();
        $.each(result, function (index, sede) {
            $("#StabilimentoId").append('<option value =' + sede.id + '>' + sede.descrizione + '</option>');
        });
    }).fail(function (ex) {
        alert('Failed to retrieve states.' + ex);
    })

};

//var promise;

var myModal;
function close_modal(value) {
    if (myModal != undefined) {
        myModal.toggle()
        console.log("value", value)
        //promise.resolve(value)
    }
}

function open_modal(id = "modal-confirm-delete") {
    var modalEl = document.getElementById(id)
    if (myModal == undefined) {
        myModal = new bootstrap.Modal(modalEl, {
            keyboard: false
        })
        //promise = new Promise((resolve) => {
        //    console.log("Promise")
        //})
    }
    myModal.toggle()
}

function active_toggler() {
    var element = document.getElementById("burger");
    element.classList.toggle("burger-menu_active");
    var element = document.getElementById("menu-home");
    element.classList.toggle("d-none");
}

function test_model(value) {
    console.log(value);
}

function resetradio(id, document) {
    console.log("document", document)
    console.log("id", id)
    console.log("document.getElementById("+ id + ")", document.getElementById(id))
    var radios = $('div[id=Radio_' + id.split("_")[1] + ']');
    if (document.getElementById(id).checked) {
        radios.removeClass("d-none")
    } else {
        radios.addClass("d-none")
    }

}

function onsubmitclick(value) {
    document.getElementById("statoCampiFissi").value = value;
};