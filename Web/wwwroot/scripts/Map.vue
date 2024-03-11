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
    import { ref, onMounted } from "vue";
	//@ts-ignore
    import mapboxgl from "mapbox-gl";

    const mapContainer = ref(null);
    let map;

    onMounted(() => {
        mapboxgl.accessToken = "pk.eyJ1Ijoibmljb25ldSIsImEiOiJjbGVzYnVwNjkwM21lNDVuemkzZDYxMDB1In0.6GUq94_v3_2Zv7aeNyJuJQ";
        map = new mapboxgl.Map({
            container: mapContainer.value,
            style: "mapbox://styles/mapbox/streets-v11"
        });
    });
</script>