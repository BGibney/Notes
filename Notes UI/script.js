

// use the fetch api to add a note to the div with the class of notes__container
function addNote(title, description){
    fetch('http://localhost:3000/notes', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({title, description})
    })
    .then(res => res.json())
    .then(data => {
        const notesContainer = document.querySelector('.notes__container');
        const note = document.createElement('div');
        note.classList.add('note');
        note.innerHTML = `
            <h3>${data.title}</h3>
            <p>${data.description}</p>
        `;
        notesContainer.appendChild(note);
    })
    .catch(err => console.log(err));
}

// use the fetch api to get all notes from the server
function getNotes(){
    fetch('http://localhost:3000/notes')
    .then(res => res.json())
    .then(data => {
        const notesContainer = document.querySelector('.notes__container');
        data.forEach(note => {
            const noteElement = document.createElement('div');
            noteElement.classList.add('note');
            noteElement.innerHTML = `
                <h3>${note.title}</h3>
                <p>${note.description}</p>
            `;
            notesContainer.appendChild(noteElement);
        });
    })
    .catch(err => console.log(err));
}
