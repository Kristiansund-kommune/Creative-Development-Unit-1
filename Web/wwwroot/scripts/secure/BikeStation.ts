import Dock from "@/scripts/Dock";

export default class BikeStation{
	StationId: number;
	Lat: number;
	Lon: number;
	BikeStationDocks : Dock[];
	TotalDocks: number;
	AvailableDocks: number;
	StationName: string;


	constructor(lat: number, lon: number, docks: Dock[], totalDock: number, availableDocks: number, StationName : string, StationId : number) {
		this.Lat = lat;
		this.Lon = lon;
		this.BikeStationDocks = docks;
		this.TotalDocks = totalDock;
		this.AvailableDocks = availableDocks;
		this.StationName = StationName;
		this.StationId = StationId;
	}





}
