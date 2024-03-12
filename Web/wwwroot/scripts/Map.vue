<template>
	<div ref="mapContainer" class="map-container"></div>
</template>

<style scoped>
    .map-container {
        height: 100%;
        width: 100%;
    }
</style>

<script setup lang="ts">
    import { ref, onMounted, watch } from "vue";
    import "../../../node_modules/mapbox-gl/dist/mapbox-gl.css";
    //@ts-ignore
    import mapboxgl from "mapbox-gl";

    const mapContainer = ref(null);
    let map;
    const clientCoordinates = ref<{lng: number, lat: number}>({ lng: 7.7271277 , lat: 63.113054});

    onMounted(() => {
        mapboxgl.accessToken = "pk.eyJ1Ijoibmljb25ldSIsImEiOiJjbGVzYnVwNjkwM21lNDVuemkzZDYxMDB1In0.6GUq94_v3_2Zv7aeNyJuJQ";

		getClientCoordinates();
		
        map = new mapboxgl.Map({
			container: mapContainer.value,
			style: "mapbox://styles/mapbox/streets-v11",
			center: [clientCoordinates.value.lng, clientCoordinates.value.lat], 
			zoom: 6,     
        });
		
    });
	
    async function getClientCoordinates(){
		navigator.geolocation.getCurrentPosition( (position) => {
			clientCoordinates.value = { lng: position.coords.longitude, lat: position.coords.latitude};
		});
	}

    watch(clientCoordinates, (newVal, oldVal) => {
        map.flyTo({center: [newVal.lng, newVal.lat], zoom: 13});
    });
</script>