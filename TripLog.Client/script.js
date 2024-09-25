let tripCounter = 1;
window.onload = GetAllTrips();

async function GetAllTrips() {
  const cards = document.getElementById("cards");

  var request = await fetch("http://localhost:5096/api/Trip/GetAll");
  var response = await request.json();
  console.log(response);

  response.$values.forEach((x, index) => {
    let row = `
    <div class="card" style="width: 50rem">
          <table>
            <tr class="satir">
              <th>
                <img src="http://localhost:5096/${x.imageUrl}" class="card-img-top" alt="..." />
              </th>
              <th>
                <div class="card-body" style="flex: 1">
                  <h5 class="card-title">${x.title}</h5>
                  <p class="card-text">
                  ${x.description}
                  </p>`;
    x.tags.$values.forEach((t) => {
      row += `
             <button type="button" class="btn btn-outline-secondary" disabled> ${t.name}</button>
              `;
    });
    row += `
    
                  <a
                    href="#"
                    class="btn btn-primary"
                    style="background-color: rgb(118, 153, 64)"
                    data-bs-toggle="collapse"
                    data-bs-target="#collapse${index}"
                    aria-expanded="true"
                    aria-controls="collapseOne"+
                    >Details</a
                  >
                </div>
              </th>
            </tr>
          </table>
          <!-- Accordion -->
          <div class="accordion" id="accordionExample">
            <div class="accordion-item">
              <div
                id="collapse${index}"
                class="accordion-collapse collapse"
                data-bs-parent="#accordionExample"
              >
                <div class="accordion-body">
                  <div
                    id="carouselExampleAutoplaying${index}"
                    class="carousel slide"
                    data-bs-ride="carousel"
                  >
                    <div class="carousel-inner">
                    `;

    x.tripPhotos.$values.forEach((y, index2) => {
      if (index2 == 0) {
        row += `<div class="carousel-item active">`;
      } else {
        row += `<div class="carousel-item ">`;
      }
      row += `
      
                        <img
                          src="http://localhost:5096/${y.photoUrl}"
                          class="d-block w-100"
                          alt="..."
                        />
                        <div class="card-body" style="flex: 1">
                          <h5 class="card-title">${y.title}</h5>
                          <p class="card-text">
                          ${y.description}
                          </p>
                        </div>
                      </div>
      `;
    });
    row += `</div>
                    <button
                      class="carousel-control-prev"
                      type="button"
                      data-bs-target="#carouselExampleAutoplaying${index}"
                      data-bs-slide="prev"
                    >
                      <span
                        class="carousel-control-prev-icon"
                        aria-hidden="true"
                      ></span>
                      <span class="visually-hidden">Previous</span>
                    </button>
                    <button
                      class="carousel-control-next"
                      type="button"
                      data-bs-target="#carouselExampleAutoplaying${index}"
                      data-bs-slide="next"
                    >
                      <span
                        class="carousel-control-next-icon"
                        aria-hidden="true"
                      ></span>
                      <span class="visually-hidden">Next</span>
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
    `;
    cards.innerHTML += row;
  });
}

async function Create() {
  const tripTitle = document.getElementById("trip-title").value;
  const tripDescription = document.getElementById("main-description").value;
  const tripTags = document.getElementById("tags").value;
  const tripMainPhoto = document.getElementById("main-photo").files[0];

  let formData = new FormData();

  formData.append("title", tripTitle);
  formData.append("description", tripDescription);
  formData.append("tags", tripTags);
  formData.append("image", tripMainPhoto);
  debugger;
  const tripPhoto = document.getElementById("tripPhotos");
  const tripParts = tripPhoto.querySelectorAll("div.tripPart");

  tripParts.forEach((content, index) => {
    debugger;
    const tripSubTitle = content.querySelector("#tripSubTitle").value;
    const tripSubDescription = content.querySelector(
      "#tripSubDescription"
    ).value;
    const tripSubImage = content.querySelector("#tripSubImage").files[0];

    formData.append(`tripPhotoDTOs[${index}].title`, tripSubTitle);
    formData.append(`tripPhotoDTOs[${index}].description`, tripSubDescription);
    formData.append(`tripPhotoDTOs[${index}].photoUrl`, tripSubImage);
  });

  const request = await fetch("http://localhost:5096/api/Trip/Create", {
    method: "POST",
    body: formData,
  });
}

function addTripPart() {
  const tripPhotos = document.getElementById("tripPhotos");
  if (tripCounter <= 5)
    tripPhotos.innerHTML += `
              <div class="tripPart">
              <br>
              <h6>${tripCounter}. Alt Başlık:</h6>
              <input type="text" class="form-control" id="tripSubTitle" placeholder="Resim başlığı giriniz.">
              <br>
              <h6>${tripCounter}. Alt Metni:</h6>
              <input type="text" class="form-control" id="tripSubDescription" placeholder="Resim metni giriniz.">
              <br>
              <h6>${tripCounter}. Alt Kapak Fotoğrafı:</h6>
              <input type="file" id="tripSubImage" class="form-control">
              <br>
              <hr class="border-3">
              </div>`;
  tripCounter++;
}

function cancelTrip() {
  const tripPhotos = document.getElementById("tripPhotos");
  tripPhotos.innerHTML = "";
  tripCounter = 1;
}
