import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";

class VaultKeepsService {
	async addKeepToVault(vaultKeepData) {
		const res = await api.post("api/vaultkeeps", vaultKeepData);
		logger.log(res.data);
		AppState.focusedKeep.kept++;
	}
}
export const vaultKeepsService = new VaultKeepsService();
