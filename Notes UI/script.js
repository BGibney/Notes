// get the button with the id saveButton
const saveButton = document.querySelector('#saveButton');
// get the input field with the title id
const title = document.querySelector('#title');
// get the textarea field with id description
const description = document.querySelector('#description');

// when the page loads call the getnotes method
window.addEventListener('load', getNotes);


// add an event listener to the saveButton to add a note when clicked
// and pass to the addNote function the values of the title and description inputs

saveButton.addEventListener('click', () => {
    addNote(title.value, description.value);
});

// set the main api entrypoint url
const api = 'https://localhost:7285/api';

// use the fetch api to add a note to the div with the class of notes__container
function addNote(title, description){
    fetch(`${api}/notes`, {
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
        console.log(data);
    })
    .catch(err => console.log(err));
}

// use the fetch api to get all notes from the server
function getNotes(){
    fetch(`${api}/notes`)
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
