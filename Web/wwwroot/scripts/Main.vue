<template>
	<div style="height: 100vh; width: 100vw">
		<Map @open-bike-station="openBikeStationWindow" :user-coordinates="clientCoordinates"></Map>
		<div class="logo-container">
			<img class="logo" src="../styles/logo.svg" alt="logo">
		</div>
		<div @click="showUserModal = true" class="m-3 user">
			<div class="person-icon-box font-s me-1">
				<i class="fa-regular fa-user icon"></i>
			</div>
		</div>
		<button type="button" class="btn btn-primary" @click="showModal = true">Show Modal</button>
		<button type="button" class="btn btn-primary" @click="launchConfetti">Celebrate!</button>

		<div v-if="showMessage" class="level-up-message">Level Up!</div>
		<div v-if="showModal" class="modal fade show d-block" style="z-index: 1050;" tabindex="-1" role="dialog">
			<div class="modal-dialog" role="document">
				<div class="modal-content">				
					<div>
						<span class="close" @click="showModal = false">&times;</span>
					</div>
					<h2>Parkering</h2>
					<div class="p-3"></div>
					<p>Plasser som er tillgejenlig:</p>
					<div class="border2 m-3 p-4">
						<div v-for="div in divs" :key="div.id" class="selectable-div" :class="{'red': div.selected, 'green': !div.selected}" @click="toggleSelection(div.id)">
							<!-- <button @click.prevent="lockStand(div.id)">
								{{ div.id }}
							</button>
							<button @click.prevent="unlockStand(div.id)">
								{{ div.id }}
							</button> -->
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div v-if="showUserModal" class="modal fade show d-block" style="z-index: 1050;" tabindex="-1" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<h5 class="modal-title">User Details</h5>
					<button type="button" class="btn-close" @click="showUserModal = false" aria-label="Close"></button>
				</div>
				<div class="modal-body">
					<div class="d-flex align-items-center text-center mb-4">
						<div class="person-icon-box me-3">
							<i class="fa-regular fa-user icon"></i>
						</div>
					</div>
            
					<hr>
            
					<div class="mb-3">
						<h6>{{ user.name }}</h6>
						<p class="text-muted">{{ user.email }}</p>
						<hr>
						<p><strong>Registration Date:</strong> {{ formatDate(user.registrationDate) }}</p>
						<p><strong>Number of Rides:</strong> {{ user.stats.numberOfRides }}</p>
						<p><strong>Total Distance:</strong> {{ user.stats.totalDistance }} meters</p>
						<p><strong>Total Time:</strong> {{ user.stats.totalTime }} minutes</p>
						<p><strong>Current Points:</strong> {{ user.stats.currentPoints }}</p>
						<p><strong>Level:</strong> {{ user.stats.level.title }}</p>
					</div>
            
					<hr>
					<p class="text-secondary">{{ user.stats.currentPoints }}/2700</p>
					<div class="progress">
						
						<div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100" style="width: 75%"></div>
					</div>
				</div>

				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" @click="showUserModal = false">Close</button>
				</div>
			</div>
		</div>
	</div>

</template>

  <style scoped>
.person-icon-box {
  /* Example styling; adjust as needed */
  display: flex;
  align-items: center;
  justify-content: center;
  width: 100px;
  height: 100px;
  font-size: 4em;
  border-radius: 50px; /* Makes it circular */
  background-color: #f0f0f0; /* Light grey background */
  margin-right: 15px;
}

.font-s{
	font-size: 2.5em !important;
	cursor: pointer;
	border: 1px solid black;
	background: white; 
	width: 100px !important;
	height: 100px !important;
}

.user {
	position: absolute;
    top: 0;
    right: 0;
}


/* Additional spacing for modal content */
.modal-content {
  padding: 20px;
}

/* Styling for horizontal rules */
hr {
  margin-top: 1rem;
  margin-bottom: 1rem;
  border: 0;
  border-top: 1px solid rgba(0,0,0,.1);
}

/* Adjustments for better spacing and appearance */
.modal-header, .modal-footer {
  border-bottom: 0;
  border-top: 0;
}

.modal-body p {
  margin-bottom: 10px; /* Adds a little more space between paragraphs */
}



  .logo-container {
    position: absolute;
    top: 0;
    left: 0;
    display: flex;
    align-items: right;
    padding: 10px 25px;
    background-color: rgb(240, 239, 239);
	
  }

  .logo {
    height: 50px;
    width: auto;
  }

  .level-up-message {
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    font-size: 3rem;
    color: yellow;
    z-index: 2;
    background-color: rgba(0, 0, 0, 0.5);
    padding: 20px;
    border-radius: 10px;
  }

  .modal {
    display: block;
    position: fixed;
    z-index: 1;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    overflow: auto;
    background-color: rgba(0, 0, 0, 0.4);
  }

  .border2 {
    border-bottom: 0 !important;
    padding: 15px;
    border: 1PX solid black;
    display: grid;
    border-radius: 10px 10px 0px 0px;
    grid-template-columns: repeat(auto-fill, minmax(45px, 0.5fr));
    gap: 20px;
  }


  .modal-content {
    background-color: #fefefe;
    margin: 15% auto;
    padding: 2%;
    border: 1px solid #888;
    width: 80%;
  }

  .close {
    color: #aaa;
    float: right;
    font-size: 28px;
    font-weight: bold;
  }

  .close:hover,
  .close:focus {
    color: black;
    text-decoration: none;
    cursor: pointer;
  }

  .selectable-div {
    text-align: center;
    padding: 0px;
    border-radius: 5px;
    cursor: pointer;
    color: white;
    height: 60px;
    width: 60px;
    font-size: 2em;
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .red {
    background-color: rgb(165, 30, 30);
  }

  .green {
    background-color: rgb(18, 110, 18);
  }
  </style>

<script setup lang="ts">
import {onMounted, ref} from "vue";
import Map from "@/scripts/Map.vue";
import confetti from "canvas-confetti";
import {useRouter} from "vue-router";
import axios from "axios";
import BikeStation from "@/scripts/secure/BikeStation";
import mapboxgl from "mapbox-gl";

const showMessage = ref(false);
const showUserModal = ref(false);
const showModal = ref(false);
const bikeStations = ref();

const clientCoordinates = ref<{lon: number, lat: number}>({ lon: 7.7271277 , lat: 63.113054});


const chosenStation = ref();

const router = useRouter();



onMounted( async () =>{
	await fetchUserData(); 
	await fetchBusStops();
	await getClientCoordinates();
} );


async function fetchUserData(){
	try {
		const idWrap = router.currentRoute.value.params;
		if (idWrap.id){
			const response = await axios.get(`/User/GetUserWithStats?Id=${idWrap.id}`);
			if (response.data) {
				const data = response.data;
				user.value = { name: data.name, email: data.email, registrationDate: new Date(data.registrationDate), stats: data.stats};
			}
		}
	}
	catch (error) {
		console.error("Failed to fetch user data:", error);
	}
}

const accessToken = "pk.eyJ1Ijoibmljb25ldSIsImEiOiJjbGVzYnVwNjkwM21lNDVuemkzZDYxMDB1In0.6GUq94_v3_2Zv7aeNyJuJQ";
async function openBikeStationWindow(name: string){
	const index = bikeStations.value?.findIndex(e => e.stationName === name);
	chosenStation.value = bikeStations.value[index];
	showModal.value = true;
}

async function getClientCoordinates(){
	navigator.geolocation.getCurrentPosition( (position) => {
		clientCoordinates.value = { lon: position.coords.longitude, lat: position.coords.latitude};
	});
}


async function fetchBusStops(){
	try {
		const response = await axios.get(`/BikeStations/Get`);
		if (response.data) {
			const data = response.data;
			bikeStations.value = data.$values;
		}
	}
	catch (error) {
		console.error("Failed to fetch busStops data:", error);
	}
}



const user = ref({
	name: "John Doe",
	email: "john.doe@example.com",
	registrationDate: new Date(), // Example: use actual data here
	stats: {
		numberOfRides: 100,
		totalDistance: 1500,
		totalTime: 75,
		currentPoints: 2000,
		evaluationTotal: 52,
		level: {
			title: "Journeyman",
			requiredPoints: 0
		},
	},
});



// Example filter for formatting dates, adjust as needed
const formatDate = (value) => {
	if (!value) {
		return "";
	}
	const date = new Date(value);
	return date.toLocaleDateString();
};

interface Div {
	id: number;
	name: string;
	selected: boolean;
}

const divs = ref<Div[]>([
	{ id: 1, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
	{ id: 2, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
	{ id: 3, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
	{ id: 4, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
]);

const lockStand = async (id: number) => {
	await axios.post(`/BikeStations/lockStand?deviceName=bikeLockPrototype&lockId=${id}`);
};

const unlockStand = async (id: number) => {
	await axios.post(`/BikeStations/unlockStand?deviceName=bikeLockPrototype&lockId=${id}`);
};

const toggleSelection = (id: number) => {
	const div = divs.value.find(d => d.id === id);
	if (div) {
		if (div.selected) {
			div.selected = false;
			unlockStand(id);
		}
		else {
			div.selected = true;
			lockStand(id);
		}
	}
};

const launchConfetti = () => {
	showMessage.value = true;

	confetti({
		particleCount: 100,
		spread: 70,
		origin: { y: 0.5 },
	});

	setTimeout(() => {
		showMessage.value = false;
	}, 1000);

	setTimeout(() => {
		confetti({
			particleCount: 200,
			spread: 220,
			origin: { y: 0.5 },
		});
	}, 500);
};
</script>  