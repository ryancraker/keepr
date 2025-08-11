import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Keep } from "@/models/Keep.js";

class KeepsService {
	clearFocusedKeep() {
		AppState.focusedKeep = null;
	}
	async getKeepById(keepId) {
		AppState.focusedKeep = null;
		const res = await api.get(`api/keeps/${keepId}`);
		logger.log(res.data);
		AppState.focusedKeep = new Keep(res.data);
	}
	async createKeep(keepData) {
		const res = await api.post("api/keeps", keepData);
		logger.log(res.data);
		AppState.keeps.unshift(new Keep(res.data));
	}
	async deleteKeep(keepId) {
		const keeps = AppState.keeps;
		const keepIndex = keeps.findIndex(keep => keep.id == keepId);
		const res = await api.delete(`api/keeps/${keepId}`);
		logger.log(res.data);
		keeps.splice(keepIndex, 1);
	}
	async getKeeps() {
		const res = await api.get("api/keeps");
		logger.log(res.data);
		AppState.keeps = res.data.map(pojo => new Keep(pojo));
	}
}
export const keepsService = new KeepsService();
