<script setup>
	import { ref, watch } from "vue";
	import { loadState, saveState } from "../utils/Store.js";
	import Login from "./Login.vue";
	import FormModalWrapper from "./FormModalWrapper.vue";
	import KeepForm from "./KeepForm.vue";
	import VaultForm from "./VaultForm.vue";
	import FocusedKeep from "./FocusedKeep.vue";
	import KeepModal from "./KeepModal.vue";

	const theme = ref(loadState("theme") || "light");

	// function toggleTheme() {
	//   theme.value = theme.value == 'light' ? 'dark' : 'light'
	// }

	watch(
		theme,
		() => {
			document.documentElement.setAttribute("data-bs-theme", theme.value);
			saveState("theme", theme.value);
		},
		{ immediate: true }
	);
</script>

<template>
	<nav class="navbar navbar-expand-md bg-light border-bottom border-vue">
		<div class="container-fluid gap-2 d-flex justify-content-between">
			<div class="d-flex">
				<RouterLink :to="{ name: 'Home' }" class="d-flex align-items-center">
					<span class="home-button">Home</span>
				</RouterLink>
				<div class="dropdown">
					<button
						class="btn dropdown-toggle"
						type="button"
						data-bs-toggle="dropdown"
						aria-expanded="false">
						Create
					</button>
					<ul class="dropdown-menu">
						<li>
							<button data-bs-target="#keepModal" data-bs-toggle="modal" class="btn" type="button">
								Keep
							</button>
						</li>
						<li>
							<button data-bs-target="#vaultModal" data-bs-toggle="modal" class="btn" type="button">
								Vault
							</button>
						</li>
					</ul>
				</div>
			</div>
			<img class="logo-pic" src="/src/assets/img/keepr-logo.png" alt="Keepr Logo" />
			<div class="d-flex justify-content-between align-items-center">
				<Login />
			</div>
		</div>
	</nav>
	<FormModalWrapper modalId="keep" modalHeader="Add your keep">
		<KeepForm />
	</FormModalWrapper>
	<FormModalWrapper modalId="vault" modalHeader="Add your vault">
		<VaultForm />
	</FormModalWrapper>
	<KeepModal modal-id="focusedKeep">
		<FocusedKeep />
	</KeepModal>
</template>

<style lang="scss" scoped>
	.modal-backdrop {
		background-color: white;
		opacity: 1;
	}
	a {
		text-decoration: none;
	}

	.logo-pic {
		height: 4rem;
	}

	.nav-link {
		text-transform: uppercase;
	}

	.home-button {
		color: black;
		background-color: rgba(202, 202, 202, 1);
		border-radius: 15px;
		padding: 0.25rem;
		padding-left: 0.5rem;
		padding-right: 0.5rem;
	}

	.navbar-nav .router-link-exact-active {
		border-bottom: 2px solid var(--bs-success);
		border-bottom-left-radius: 0;
		border-bottom-right-radius: 0;
	}
</style>
