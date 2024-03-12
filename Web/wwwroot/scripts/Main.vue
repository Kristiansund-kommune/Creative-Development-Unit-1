<template>
	<div style="height: 100vh; width: 100vw">
		<Map></Map>
		<div class="logo-container">
			<img class="logo" src="../styles/logo.svg" alt="logo">
		</div>
		<button @click="showModal = true">Show Modal</button>
		<button @click="launchConfetti">Celebrate!</button>
		<div v-if="showMessage" class="level-up-message">Level Up!</div>
		<div v-if="showModal" class="modal">
			<div class="modal-content">				
				<div>
					<span class="close" @click="showModal = false">&times;</span>
				</div>
				<h2>Parkering</h2>
				<div class="p-3"></div>
				<p>Plasser som er tillgejenlig:</p>
				<div class="border2 m-3 p-4">
					<div v-for="div in divs" :key="div.id" class="selectable-div" :class="{'red': div.selected, 'green': !div.selected}" @click="toggleSelection(div.id)" v-html="div.name">
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<style scoped>
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
  z-index: 2; /* Ensure it's visible above other elements */
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
  grid-template-columns: repeat(auto-fill, minmax(80px, 0.5fr)); /* Creates a responsive grid where each cell has a min width of 120px */
  gap: 20px; /* Adjusts the gap between grid items */
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
  color: white; /* White text */
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
import { ref } from "vue";
import Map from "@/scripts/Map.vue";
import confetti from "canvas-confetti";

const showMessage = ref(false);

const showModal = ref(false);

interface Div {
	id: number;
	name: string;
	selected: boolean;
}

const divs = ref<Div[]>([
	{ id: 1, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
	{ id: 2, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
	{ id: 3, name: '<i class="fa-solid fa-square-parking"></i>', selected: true },
	{ id: 4, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
	{ id: 5, name: '<i class="fa-solid fa-square-parking"></i>', selected: true },
	{ id: 6, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
	{ id: 7, name: '<i class="fa-solid fa-square-parking"></i>', selected: true },
	{ id: 8, name: '<i class="fa-solid fa-square-parking"></i>', selected: true },
	{ id: 9, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
	{ id: 0, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
	{ id: 22, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
	{ id: 42, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
	{ id: 29, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
	{ id: 20, name: '<i class="fa-solid fa-square-parking"></i>', selected: false },
]);

const toggleSelection = (id: number) => {
	const div = divs.value.find(d => d.id === id);
	if (div) {
		div.selected = !div.selected;
	}
};

const launchConfetti = () => {
  // Show the "Level Up!" message
  showMessage.value = true;

  // Launch confetti immediately
  confetti({
    particleCount: 100,
    spread: 70,
    origin: { y: 0.5 },
  });

  // Hide the message after 1 second
  setTimeout(() => {
    showMessage.value = false;
  }, 1000);

  // Launch confetti again after a 0.5-second delay
  setTimeout(() => {
    confetti({
      particleCount: 200,
      spread: 220,
      origin: { y: 0.5 },
    });
  }, 500);
};
</script>