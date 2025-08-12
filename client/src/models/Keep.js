import { Account } from "./Account.js";

export class Keep {
	constructor(data) {
		this.id = data.id;
		this.name = data.name;
		this.description = data.description;
		this.creator = new Account(data.creator);
		this.creatorId = data.creatorId;
		this.img = data.img;
		this.createdAt = data.createdAt;
		this.updatedAt = data.updatedAt;
		this.views = data.views;
		this.kept = data.kept;
		this.vaultKeepId = data.vaultKeepId;
	}
}
