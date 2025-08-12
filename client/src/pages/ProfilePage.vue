<script setup>
	import { AppState } from "@/AppState.js";
	import KeepCard from "@/components/KeepCard.vue";
	import VaultCard from "@/components/VaultCard.vue";
	import { profilesService } from "@/services/ProfilesService.js";
	import { logger } from "@/utils/Logger.js";
	import { Pop } from "@/utils/Pop.js";
	import { computed, onMounted } from "vue";
	import { useRoute } from "vue-router";

	onMounted(() => {
		getProfileById();
		getVaultsByProfileId();
		getKeepsByProfileId();
	});

	const profile = computed(() => AppState.profile);
	const route = useRoute();
	const profileId = route.params.profileId;
	const vaults = computed(() => AppState.vaults);
	const keeps = computed(() => AppState.keeps);

	async function getProfileById() {
		try {
			await profilesService.getProfileById(profileId);
		} catch (error) {
			Pop.error(error);
			logger.error(error, "could not get profile");
		}
	}
	async function getVaultsByProfileId() {
		try {
			await profilesService.getVaultsByProfileId(profileId);
		} catch (error) {
			Pop.error(error);
			logger.error(error, "could not get profile vaults");
		}
	}

	async function getKeepsByProfileId() {
		try {
			await profilesService.getKeepsByProfileId(profileId);
		} catch (error) {
			Pop.error(error);
			logger.error(error, "could not get profile keeps");
		}
	}
</script>

<template>
	<section v-if="profile" class="container profile-header">
		<div class="row profile-header">
			<div class="col-12 text-center position-relative profile-header">
				<img class="cover-img" :src="profile.coverImg" :alt="`${profile.name}'s cover image'`" />
				<img class="creator-pic" :src="profile.picture" :alt="`${profile.name}'s profile image'`" />
			</div>
			<div class="col-12 text-center">
				<p class="text-center fw-bold fs-1">{{ profile.name }}</p>
				<span class="fs-5">{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</span>
			</div>
			<div class="col-12">
				<p class="fs-1 fw-bold">Vaults</p>
				<div class="row">
					<div v-for="vault in vaults" :key="vault.id" class="col-3 mb-2">
						<VaultCard :vault />
					</div>
				</div>
			</div>
			<div class="col-12">
				<p class="fs-1 fw-bold">Keeps</p>
				<div class="row">
					<div class="col-3 mb-2 profile-keep" v-for="keep in keeps" :key="keep.id">
						<KeepCard :keep />
					</div>
				</div>
			</div>
		</div>
	</section>
</template>

<style lang="scss" scoped>
	.cover-img {
		margin-top: 5dvh;
		object-fit: cover;
		object-position: center;
		width: 80%;
		height: 35dvh;
		border-radius: 10px;
	}
	.creator-pic {
		height: 20dvh;
		object-fit: cover;
		position: absolute;
		bottom: -25%;
		left: 43%;
		border: solid thin white;
		box-shadow: 0px 0px 4px black;
	}
	.profile-header {
		margin-bottom: 10dvh;
	}
	.profile-keep {
		height: 10rem;
	}
</style>
