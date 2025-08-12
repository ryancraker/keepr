import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Vault } from "@/models/Vault.js";
import { Keep } from "@/models/Keep.js";

class VaultsService {
	async getKeepsByVaultId(vaultId) {
		AppState.keeps = [];
		const res = await api.get(`api/vaults/${vaultId}/keeps`);
		logger.log(res.data);
		AppState.keeps = res.data.map(pojo => new Keep(pojo));
	}
	async getVaultById(vaultId) {
		const res = await api.get(`api/vaults/${vaultId}`);
		logger.log(res.data);
		AppState.focusedVault = new Vault(res.data);
	}
}
export const vaultsService = new VaultsService();
