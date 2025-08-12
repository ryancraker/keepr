import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";

class VaultKeepsService {
	async deleteVaultKeep(vaultKeepId) {
		const vaultKeepIndex = AppState.keeps.findIndex(keep => keep.vaultKeepId == vaultKeepId);
		const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`);
		logger.log(res.data);
		AppState.keeps.splice(vaultKeepIndex, 1);
	}
	async addKeepToVault(vaultKeepData) {
		const res = await api.post("api/vaultkeeps", vaultKeepData);
		logger.log(res.data);
		AppState.focusedKeep.kept++;
	}
}
export const vaultKeepsService = new VaultKeepsService();
