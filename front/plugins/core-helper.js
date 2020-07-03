import Vue from 'vue';

let core = {
    isNotNull: (item) => {
        return typeof item != "undefined" &&
            item != null &&
            item.length != null &&
            item.length > 0;
    },

    //
    // Message box
    //
    message: {
        ok: (msg) => {
            if (!msg) msg = 'Operação realizada com sucesso';
            let toast = Vue.swal.mixin({ toast: true, position: 'top-end', showConfirmButton: false, timer: 3000 });
            toast({ type: 'success', title: msg });
        },
        error: (msg) => {
            let toast = Vue.swal.mixin({ toast: true, position: 'top-end', showConfirmButton: false, timer: 3000 });
            toast({ type: 'error', title: msg });
        },
    },

    //
    // Success messagebox
    //
    messageSuccess: () => {
        Vue.swal.fire({ type: 'success', text: 'Operação realizada com sucesso' });
    },

    //
    // Error messagebox
    //
    messageError: (msg) => {
        Vue.swal.fire({
            type: 'error',
            title: 'Oops...',
            text: msg
        });
    },

    //
    // Validação de CPF
    //
    checkCPF: (cpf) => {
        if (!cpf ||
            cpf.length != 11 ||
            cpf == "00000000000" ||
            cpf == "11111111111" ||
            cpf == "22222222222" ||
            cpf == "33333333333" ||
            cpf == "44444444444" ||
            cpf == "55555555555" ||
            cpf == "66666666666" ||
            cpf == "77777777777" ||
            cpf == "88888888888" ||
            cpf == "99999999999"
        )
            return false;
        var soma = 0;
        var resto;
        for (var i = 1; i <= 9; i++)
            soma = soma + parseInt(cpf.substring(i - 1, i)) * (11 - i);
        resto = (soma * 10) % 11;
        if (resto == 10 || resto == 11) resto = 0;
        if (resto != parseInt(cpf.substring(9, 10))) return false;
        soma = 0;
        for (var i = 1; i <= 10; i++)
            soma = soma + parseInt(cpf.substring(i - 1, i)) * (12 - i);
        resto = (soma * 10) % 11;
        if (resto == 10 || resto == 11) resto = 0;
        if (resto != parseInt(cpf.substring(10, 11))) return false;
        return true;
    },

    //
    // Verifica e Valida a Data de Nascimento
    //
    checkBirthday: (birthday, minimalAge) => {
        if (birthday) {
            //console.log("checkBirthday", birthday);
            //https://pt.stackoverflow.com/questions/296442/validar-data-de-nascimento-para-que-n%C3%A3o-possa-registrar-menores-de-18-anos-jav
            //obter array com [ano,mes,dia] através de split("-") e convertendo em numero com Map
            let [dia, mes, ano] = birthday.split("/").map(Number);
            //construir data 18 anos a seguir a data dada pelo usuario
            let depois18Anos = new Date(ano + Number(minimalAge), mes - 1, dia);
            //let depois100Anos = new Date(ano + 100, mes - 1, dia);
            let agora = new Date();

            if (depois18Anos <= agora) {
                return true;
            } else {
                return false;
            }
        }
        return false;
    },

    //
    // Exclusivo do Layout -> Para Abrir e fechar o Menu Lateral Esquerdo!
    //
    enlargeMenu: () => {
        if (window.mobilecheck()) {
            if (document.body.classList.contains("sidebar-enable")) {
                document.body.classList.remove("sidebar-enable");
                return false;
            } else {
                document.body.classList.add("sidebar-enable");
                return true;
            }
        } else {
            if (document.body.classList.contains("enlarged")) {
                document.body.classList.remove("enlarged");
                return false;
            } else {
                document.body.classList.add("enlarged");
                return true;
            }
        }
    },

    //
    // Exporta para Excel baseado em Selecionar uma "Table"
    //
    exportDataTableToExcel(selector, filename = '') {
        var downloadLink;
        var dataType = "application/vnd.ms-excel;base64";
        var tableSelect = document.querySelector(selector); //document.getElementById(tableID);
        if (tableSelect) {
            var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

            // Specify file name
            filename = filename ? `${filename}.xls` : "excel_data.xls";

            // Create download link element
            downloadLink = document.createElement("a");
            document.body.appendChild(downloadLink);

            if (navigator.msSaveOrOpenBlob) {
                var blob = new Blob(['\ufeff', tableHTML], {
                    type: dataType
                });
                navigator.msSaveOrOpenBlob(blob, filename);
            } else {
                // Create a link to the file
                downloadLink.href = `data:${dataType}, ${btoa(tableHTML)}`;

                // Setting the file name
                downloadLink.download = filename;

                //triggering the function
                downloadLink.click();
            }
        }
    },

    //
    // Formata a Data do Formato pt-BR dd/mm/yyyy para yyyy-mm-dd
    //
    formatDateStringBR(date) {
        if (date) {
            const day = date.split("/")[0];
            const month = date.split("/")[1];
            const year = date.split("/")[2];

            return `${year}-${("0" + month).slice(-2)}-${("0" + day).slice(-2)}`;
            // Utilizo o .slice(-2) para garantir o formato com 2 digitos.
        }
    },


    //
    // Formata a Data do padrão yyyy-mm-dd para dd/mm/yyyy
    //
    formatDate(date) {
        //console.log("formatDate", date);
        if (!date) return null;
        if (date.length < 10) return null;

        const [year, month, day] = date.substr(0, 10).split("-");
        return `${day}/${month}/${year}`; // Irá Retornar DD/MM/YYYY
    },

    //
    // Formata a Data do Formato pt-BR dd/mm/yyyy para yyyy-mm-dd
    //
    parseDate(date) {
        //console.log("parseDate", date);
        if (!date) return null;
        if (date.length < 10) return null;

        const [day, month, year] = date.split("/");
        return `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`; // Irá Retornar YYYY/MM/DD
    },

    parseISODate(date) {
        // Order date esta vindo: 2020-06-21T00:00:00-03:00

        //console.log("parseISODate", date);
        if (!date) return null;
        if (date.length < 10) return null;

        const [year, month, day] = date.split("T")[0].split("-");

        if (day && month && year) {
            return `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;
        }
        return null;
    },

    //
    // Converte input file em base64
    //
    getBase64: (file) => {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result);
            reader.onerror = error => reject(error);
        });
    },

    sortStringDatetime(stringDates, property) {
        if (stringDates && stringDates.length > 0) {
            const reverseDateRepresentation = date => {
                let parts = date.split('/');
                return `${parts[0]}/${parts[1]}/${parts[2].split(" ")[0]} ${parts[2].split(" ")[1].split(":")[0]}:${parts[2].split(" ")[1].split(":")[1]}`;
            };

            const compare = (a, b) => {
                //console.log("a", a);
                let aComps = a[property].split(" ")[0].split("/");
                let aTimes = a[property].split(" ")[1].split(":");
                let bComps = b[property].split(" ")[0].split("/");
                let bTimes = b[property].split(" ")[1].split(":");

                let aDate = new Date(aComps[2], aComps[1], aComps[0], aTimes[0], aTimes[1]);
                let bDate = new Date(bComps[2], bComps[1], bComps[0], bTimes[0], bTimes[1]);

                return bDate.getTime() - aDate.getTime();
            }

            return stringDates.sort(compare);
        }
        return stringDates;
    },

    sortStringISODatetime(stringDates, property) {
        if (stringDates && stringDates.length > 0) {
            // 2020-06-09T18:49:04.030Z
            const reverseDateRepresentation = date => {
                let parts = date.split("-");
                return `${parts[0]}-${parts[1]}-${parts[2].split("T")[0]}T${parts[2].split("T")[1].split(":")[0]}:${parts[2].split("T")[1].split(":")[1]}:${parts[2].split("T")[1].split(":")[2].split(".")[0]}.${parts[2].split("T")[1].split(":")[2].split(".")[1]}`;
            };

            const compare = (a, b) => {
                //console.log("a", a);
                let aComps = a[property].split("T")[0].split("-");
                let aTimes = a[property].split("T")[1].split(":");
                let bComps = b[property].split("T")[0].split("/");
                let bTimes = b[property].split("T")[1].split(":");

                let aDate = new Date(aComps[2], aComps[1], aComps[0], aTimes[0], aTimes[1]);
                let bDate = new Date(bComps[2], bComps[1], bComps[0], bTimes[0], bTimes[1]);

                return bDate.getTime() - aDate.getTime();
            }

            return stringDates.sort(compare);
        }
        return stringDates;
    }

};
Vue.prototype.$core = core;