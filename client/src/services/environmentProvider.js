
const env = require("../environment.config.json");

export async function getNavTree() {
    const response = await fetch(env.serverAddress + "/api/environment", {
        mode: "cors"
    });
    return await response.json();
}