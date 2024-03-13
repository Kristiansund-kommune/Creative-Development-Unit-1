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
import {ref, onMounted, watch,} from "vue";
    import "../../../node_modules/mapbox-gl/dist/mapbox-gl.css";
    //@ts-ignore
    import mapboxgl from "mapbox-gl";
import {cloneDeep} from "lodash";
import axios from "axios";


const mapContainer = ref(null);
    const map = ref(null);
    const clientCoordinates = ref<{lng: number, lat: number}>({ lng: 7.7271277 , lat: 63.113054});


	const emit = defineEmits<{
		(e: "openBikeStation", name: string): void
	}>();
	
    onMounted(async () => {
        mapboxgl.accessToken = "pk.eyJ1Ijoibmljb25ldSIsImEiOiJjbGVzYnVwNjkwM21lNDVuemkzZDYxMDB1In0.6GUq94_v3_2Zv7aeNyJuJQ";

		await getClientCoordinates();
		const stops = await fetchBusStops();
	
		map.value = new mapboxgl.Map({
			container: mapContainer.value,
			style: "mapbox://styles/mapbox/streets-v11",
			center: [clientCoordinates.value.lng, clientCoordinates.value.lat],
			zoom: 6,
		});

		map.value.on("load", async () => {
			map.value.loadImage(
				"https://docs.mapbox.com/mapbox-gl-js/assets/custom_marker.png",
				(error, image) => {
					if (error) {
						throw error;
					}
					map.value.addImage("custom-marker", image);
					// Add a GeoJSON source with 2 points
					
					map.value.on("click", "bikestations", e => {
						const features = map.value.queryRenderedFeatures(e.point, { layers: ["bikestations"] });

						if (!features.length) {
							return;
						}

						const clickedFeature = features[0];
						// Do something with the clicked feature
						
						emit("openBikeStation", clickedFeature.properties.title);
					});
					map.value.addSource("bikestations", generateFeaturePoints(stops));

					// Add a symbol layer
					map.value.addLayer({
						"id": "bikestations",
						"type": "symbol",
						"source": "bikestations",
						"layout": {
							"icon-image": "custom-marker",
							// get the title name from the source's "title" property
							"text-field": ["get", "title"],
							"text-font": [
								"Open Sans Semibold",
								"Arial Unicode MS Bold"
							],
							"text-offset": [0, 1.25],
							"text-anchor": "top"
						}
					});
				}
			);
			
			
			
			
			
			
			
			
			
			
			
			/*if (stops){
				stops.map(marker => {
					new mapboxgl.Marker()
						.setLngLat([marker.lat, marker.lon])
						.on("click", () => {
							console.log("Click");
})
						.addTo(map.value);
				});
			}*/

		});
	
		
    });


	
	


	
    async function getClientCoordinates(){
		navigator.geolocation.getCurrentPosition( (position) => {
			clientCoordinates.value = { lng: position.coords.longitude, lat: position.coords.latitude};
		});
	}




function generateFeaturePoints(data): any{
	const baseFeature = {
		"type": "Feature",
		"geometry": {
			"type": "Point",
			"coordinates": [
				-77.03238901390978, 38.913188059745586
			]
		},
		"properties": {
			"title": "Mapbox DC",
			"ids" : 0
		}
	};
		
	const baseElement = {
		"type" : "geojson",
		"data": {
			"type": "FeatureCollection",
			"features": []
		}
	};
	
	const el = cloneDeep(baseElement);
	
	
	el.data.features = data.map(e => {
		const bf = cloneDeep(baseFeature);
		bf.geometry.coordinates = [ e.lat, e.lon ];
		bf.properties.title = e.stationName;
		bf.properties.id = e.id;
		return bf;
	});
	
	return el;
}

//
async function fetchBusStops(){
	try {
		const response = await axios.get(`/BikeStations`);
		if (response.data) {
			const data = response.data.$values;
			return data;
			//return data.map(e => new BikeStation(e.Lat, e.Lon, e.BikeStationDocks, e.TotalDocks, e.AvailableDocks, e.StationName));
		}
		return undefined;
	}
	catch (error) {
		console.error("Failed to fetch busStops data:", error);
	}
}


watch(clientCoordinates, (newVal, oldVal) => {
        map.value.flyTo({center: [newVal.lng, newVal.lat], zoom: 13});
    });
</script>