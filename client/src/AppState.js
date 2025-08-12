import { reactive } from "vue";

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
	/**@type {import('@bcwdev/auth0provider-client').Identity} */
	identity: null,
	/** @type {import('./models/Account.js').Account} user info from the database*/
	account: null,
	/**@type {import('./models/Keep.js').Keep[]} */
	keeps: [],
	/**@type {import('./models/Keep.js').Keep} */
	focusedKeep: null,
	/**@type {import('./models/Vault.js').Vault[]} */
	myVaults: [],
	/** @type {import('./models/Account.js').Account} user info from the database*/
	profile: null,
	vaults: [],
	focusedVault: null
});
