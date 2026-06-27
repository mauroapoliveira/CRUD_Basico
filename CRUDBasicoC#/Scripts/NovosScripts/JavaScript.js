
    // Usamos o ID que definimos acima
const inputFile = document.querySelector("#NomeArquivo");

inputFile.addEventListener("change", function (e) {
    const inputTarget = e.target;
    const file = inputTarget.files[0];

    // Verificamos se um arquivo foi realmente selecionado
    if (file) {
        const reader = new FileReader();

        reader.addEventListener("load", function (e) {
            const readerTarget = e.target;
            const img = document.querySelector("#img");

            if (img) {
                img.src = readerTarget.result;
            }

            const figcaption = document.querySelector("#figcaption");
            if (figcaption) {
                figcaption.innerHTML = file.name;
            }
        });
        reader.readAsDataURL(file);
    }
});
