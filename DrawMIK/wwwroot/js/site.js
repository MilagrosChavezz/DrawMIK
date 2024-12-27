var connection = new signalR.HubConnectionBuilder()
    .withUrl("/painterhub")
    .build();

const canvas = document.getElementById("drawingCanvas");
const ctx = canvas.getContext('2d');
let isDrawing = false;
let startX = 0;
let startY = 0;
let lines = [];
let savedLines = [];

document.getElementById("SaveDraw").style.display = "none";

function loadDrawing(savedLines, ctx, connection, lines) {
    savedLines.forEach(line => {
        const { startX, startY, endX, endY } = line;
        ctx.beginPath();
        ctx.moveTo(startX, startY);
        ctx.lineTo(endX, endY);
        ctx.stroke();
        connection.invoke("SendDraw", startX, startY, endX, endY).catch(function (err) {
            console.error(err.toString());
        });
        lines.push({ startX, startY, endX, endY });
    });
}

document.addEventListener("DOMContentLoaded", function () {
    if (savedLines !== null) {
        loadDrawing(savedLines, ctx, connection, lines);
    }

    canvas.addEventListener("mousedown", function (e) {
        isDrawing = true;
        startX = e.offsetX;
        startY = e.offsetY;
    });

    canvas.addEventListener("mousemove", function (e) {
        if (isDrawing) {
            const pos1 = e.offsetX;
            const pos2 = e.offsetY;

            ctx.beginPath();
            ctx.moveTo(startX, startY);
            ctx.lineTo(pos1, pos2);
            ctx.stroke();

            connection.invoke("SendDraw", startX, startY, pos1, pos2).catch(function (err) {
                return console.error(err.toString());
            });

            lines.push({ startX, startY, endX: pos1, endY: pos2 });
            startX = pos1;
            startY = pos2;
        }
    });

    canvas.addEventListener("mouseup", function () {
        isDrawing = false;
    });

    canvas.addEventListener("mouseleave", function () {
        isDrawing = false;
    });

    document.getElementById('formDrawing').addEventListener('click', function (e) {
        e.preventDefault();

        let x = document.getElementById('SaveDraw');
        if (x.style.display === "none") {
            x.style.display = "block";
        } else {
            x.style.display = "none";
            xForm.reset();
        }


    }
    );

    document.getElementById('savedrawing').addEventListener('click', function (e) {
        e.preventDefault();

        let drawingName = document.getElementById('drawingName').value;
        fetch('/Drawing/SaveDrawing', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify({
                lines: lines,
                drawingName: drawingName
            })
        })
            .then(response => {
                if (response.ok) {
                    console.log('Dibujo guardado exitosamente.');
                    lines = [];
                } else {
                    console.error('Error al guardar el dibujo.');
                }
            })
            .catch(error => {
                console.error('Error en la solicitud:', error);
            });
    });

    connection.start()
        .then(function () {
            console.log("Conectado");
        })
        .catch(function (err) {
            console.error("Error al iniciar la conexión:", err);
        });
});
