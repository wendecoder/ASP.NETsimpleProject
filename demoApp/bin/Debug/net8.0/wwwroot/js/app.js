document.addEventListener("DOMContentLoaded", function () {
    loadTasks();
});

function loadTasks() {
    // Make a GET request to fetch tasks from the backend
    fetch("/api/tasks")
        .then(response => response.json())
        .then(tasks => {
            // Update the UI with the fetched tasks
            displayTasks(tasks);
        })
        .catch(error => console.error("Error fetching tasks:", error));
}

function addTask() {
    const taskInput = document.getElementById("taskInput");
    const taskText = taskInput.value;

    if (taskText !== "") {
        // Make a POST request to add a new task
        fetch("/api/tasks", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(taskText)
        })
        .then(response => response.json())
        .then(newTask => {
            // Update the UI with the newly added task
            displayTask(newTask);
        })
        .catch(error => console.error("Error adding task:", error));

        // Clear the input field
        taskInput.value = "";
    }
}
function deleteTask(taskId) {
    // Make a DELETE request to remove the task
    console.log(taskId);
    fetch(`/api/tasks/${taskId}`, {
        method: "DELETE"
    })
    .then(response => {
        if (response.ok) {
            // Remove the task from the UI
            const taskElement = document.getElementById(`task-${taskId}`);
            taskElement.remove();
        } else {
            console.error("Error deleting task:", response.status);
        }
    })
    .catch(error => console.error("Error deleting task:", error));
}
function displayTasks(tasks) {
    // Update the UI to display the list of tasks
    const taskList = document.getElementById("taskList");
    taskList.innerHTML = "";

    tasks.forEach(task => {
        displayTask(task);
    });
}

function displayTask(task) {
    // Create HTML elements to represent a task and append to the task list
    const taskList = document.getElementById("taskList");
    const taskItem = document.createElement("li");
    const deleteButton = document.createElement("button");

    taskItem.id = `task-${task.id}`;
    taskItem.textContent = task.text;

    deleteButton.textContent = "Delete";
    deleteButton.classList.add("delete-button"); 
    deleteButton.addEventListener("click", () => deleteTask(task.id));

    taskItem.appendChild(deleteButton);
    taskList.appendChild(taskItem);
}
