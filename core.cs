function Glass::init(%context) {
	if(!isObject(Glass)) {
		new ScriptObject(Glass) {
			version = "3.2.1-alpha+index";
			address = "api.blocklandglass.com"; //api address
			netAddress = "blocklandglass.com"; //url address
			enableCLI = true;

			liveAddress = "blocklandglass.com";
			livePort = 27002;
		};
	}

	if(isFile("Add-Ons/System_BlocklandGlass/dev/config.json")) {
		exec("./support/jettison.cs");
		%err = jettisonReadFile("Add-Ons/System_BlocklandGlass/dev/config.json");
		if(%err) {
			error("Unable to read dev config");
		} else {
			warn("Using dev config!");
			%config = $JSON::Value;
			Glass.address = %config.address;
			Glass.netAddress = %config.netAddress;

			Glass.liveAddress = %config.liveAddress;
			Glass.livePort = %config.livePort;

			Glass.devMode = true;
		}
	}

	if(%context $= "client") {
		Glass::execClient();
	} else {
		Glass::execServer();
	}
}

function Glass::debug(%text) {
	if(Glass.dev) {
		echo(%text);
	}
}

function JettisonObject::get(%this, %key) {
	return %this.value[%key];
}
