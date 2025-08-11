import { Account } from "./Account.js";

export class Vault {
	constructor(data) {
		this.id = data.id;
		this.name = data.name;
		this.description = data.description;
		this.creator = new Account(data.creator);
		this.creatorId = data.creatorId;
		this.coverImg = data.coverImg;
		this.createdAt = data.createdAt;
		this.updatedAt = data.updatedAt;
		this.isPrivate = data.isPrivate;
		this.views = data.views;
		this.kept = data.kept;
	}
}
