<script setup>
	import { AppState } from "@/AppState.js";
	import { accountService } from "@/services/AccountService.js";
	import { logger } from "@/utils/Logger.js";
	import { Pop } from "@/utils/Pop.js";
	import { Modal } from "bootstrap";
	import { computed, ref } from "vue";

	const account = computed(() => AppState.account);

	const editableAccountData = ref({
		name: account.value?.name,
		picture: account.value?.picture,
		coverImg: account.value?.coverImg
	});
	async function updateAccount() {
		try {
			await accountService.updateAccount(editableAccountData.value);
			Modal.getOrCreateInstance("#accountModal").hide();
		} catch (error) {
			Pop.error(error);
			logger.error("could not update account", error);
		}
	}
</script>

<template>
	<form @submit.prevent="updateAccount()">
		<section class="container">
			<div class="row">
				<div class="col-12">
					<div class="row">
						<div class="mb-3 col-12">
							<label for="name" class="form-label">Name</label>
							<input
								v-model="editableAccountData.name"
								type="text"
								class="form-control"
								id="name" />
						</div>
						<div class="mb-3 col-6">
							<label for="picture" class="form-label">Profile Picture</label>
							<input
								v-model="editableAccountData.picture"
								type="url"
								class="form-control"
								id="picture" />
							<div class="text-center">
								<img class="img-fluid" :src="editableAccountData.picture" alt="" />
							</div>
						</div>
						<div class="col-6 mb-3">
							<label for="cover-img" class="form-label">Cover Image</label>
							<input
								v-model="editableAccountData.coverImg"
								type="url"
								class="form-control"
								id="cover-img" />
							<div>
								<img class="img-fluid" :src="editableAccountData.coverImg" alt="" />
							</div>
						</div>
					</div>
					<button type="submit">Submit</button>
				</div>
			</div>
		</section>
	</form>
</template>

<style lang="scss" scoped></style>
