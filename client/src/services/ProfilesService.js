import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Account } from "@/models/Account.js";
import { Vault } from "@/models/Vault.js";
import { Keep } from "@/models/Keep.js";

class ProfilesService {
	async getKeepsByProfileId(profileId) {
		AppState.keeps = [];
		const res = await api.get(`api/profiles/${profileId}/keeps`);
		logger.log(res.data);
		AppState.keeps = res.data.map(pojo => new Keep(pojo));
	}
	async getVaultsByProfileId(profileId) {
		AppState.vaults = [];
		const res = await api.get(`api/profiles/${profileId}/vaults`);
		logger.log(res.data);
		AppState.vaults = res.data.map(pojo => new Vault(pojo));
	}
	async getProfileById(profileId) {
		AppState.profile = null;
		const res = await api.get(`api/profiles/${profileId}`);
		logger.log(res.data);
		AppState.profile = new Account(res.data);
	}
}
export const profilesService = new ProfilesService();
