<template>
	<Top></Top>
	<div class="container py-5">
		<div class="row justify-content-center">
			<div class="col-md-6">
				<div class="card">
					<div class="card-body">
						<h5 class="card-title text-center mb-4">Login</h5>
						<form @submit.prevent="handleLogin">
							<div class="mb-3">
								<label for="email" class="form-label">Email</label>
								<input type="email" class="form-control" id="email" v-model="loginData.email" required>
							</div>
							<div class="mb-3">
								<label for="password" class="form-label">Password</label>
								<input type="password" class="form-control" id="password" v-model="loginData.password" required>
							</div>
							<button type="submit" class="btn btn-primary w-100 my-1" @onclick="handleLogin">Login</button>
							<button class="btn btn-warning w-100 my-1" @click.prevent="handleLoginStrava">Login with Strava</button>
							<img class="w-50 p-4" src="../styles/powered-by-strava.png" alt="Powered  by strava">
						</form>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>
  


<style scoped>

</style>

<script setup lang="ts">

import { ref } from "vue";
import { useRouter } from "vue-router";
import Top from "@/scripts/top.vue";



const router = useRouter();
async function handleLoginStrava() {
	// Add axios call to /User/RedirectUserToStrava here
	//router.
	window.location.href = "http://localhost:5291/User/RedirectUserToStrava";
}



const loginData = ref({
  email: "",
  password: "",
});



function isValidEmail(email: string): boolean {
  const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return regex.test(email);
}

async function handleLogin() {
  const { email, password } = loginData.value;

  if (!isValidEmail(email)) {
    alert("Please enter a valid email address.");
    return;
  }

  const loginSuccess = await verifyLogin(email, password);
  if (loginSuccess) {
    router.push("/main");
  }
 else {
    alert("Login failed. Please check your email and password.");
  }
}


async function verifyLogin(email: string, password: string): Promise<boolean> {
  if (email && password) {
    return true;
  }
  return false;
}
</script>