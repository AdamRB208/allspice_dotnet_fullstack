<script setup>
import { computed, ref, watch } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { AppState } from '@/AppState.js';
import CreateModal from './CreateModal.vue';
import SearchForm from './SearchForm.vue';


const account = computed(() => AppState.account)
const recipe = computed(() => AppState.recipes)
const theme = ref(loadState('theme') || 'light')

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
}

watch(theme, () => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}, { immediate: true })

</script>

<template>
  <nav class="navbar navbar-expand-md bg-codeworks border-bottom border-vue">
    <div class="container gap-2">
      <RouterLink :to="{ name: 'Home' }" class="d-flex align-items-center text-light">
        <img class="navbar-brand" alt="logo" src="/img/cw-logo.png" height="45" />
        <b class="fs-5">All Spice</b>
      </RouterLink>
      <!-- collapse button -->
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-links"
        aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
        <span class="mdi mdi-menu text-light"></span>
      </button>
      <!-- collapsing menu -->
      <div class="collapse navbar-collapse " id="navbar-links">
        <ul class="navbar-nav">
          <!-- <li>
            <RouterLink :to="{ name: 'About' }" class="btn text-green selectable">
              About
            </RouterLink>
          </li> -->
        </ul>
        <span class="ms-auto me-3 d-flex justify-content-center">
          <SearchForm />
        </span>
        <!-- LOGIN COMPONENT HERE -->
        <div class="ms-auto">
          <button class="btn text-light" @click="toggleTheme"
            :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
            <i v-if="theme == 'dark'" class="mdi mdi-weather-sunny"></i>
            <i v-if="theme == 'light'" class="mdi mdi-weather-night"></i>
          </button>
        </div>
        <Login />
      </div>
    </div>
  </nav>
  <section class="container-fluid">
    <div class="row">
      <div class="col-md-12 d-flex flex-column header-img mb-5">
        <h1 class="d-flex justify-content-center text-light m-5">All Spice</h1>
        <div class="d-flex justify-content-center align-items-end">
          <RouterLink :to="{ name: 'Home' }">
            <button class="align-self-center btn btn-outline-success rounded-pill p-2 m-1">All Recipes</button>
          </RouterLink>
          <RouterLink v-if="account && account.id" :to="{ name: 'CreatorRecipes', params: { creatorId: account.id } }">
            <button class="align-self-center btn btn-outline-success rounded-pill p-2 m-1">My Recipes</button>
          </RouterLink>
          <RouterLink v-if="account && account.id" :to="{ name: 'Favorites', params: { accountId: account.id } }">
            <button class="align-self-center btn btn-outline-success rounded-pill p-2 m-1">Favorites</button>
          </RouterLink>
          <button v-if="account" class="align-self-center btn btn-outline-success rounded-pill p-2 m-1"
            data-bs-toggle="modal" data-bs-target="#createModal" type="button">Create</button>
        </div>
      </div>
    </div>
  </section>
  <CreateModal />
</template>

<style lang="scss" scoped>
a {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
.col-md-12 {
  min-height: 40dvh;
}

.header-img {
  background-image: url(https://images.unsplash.com/photo-1714139314634-852891bdf2d1?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTk0fHxpbmdyZWRpZW50cyUyMG9uJTIwY2hvcHBpbmclMjBib2FyZHxlbnwwfDB8MHx8fDI%3D);
  background-repeat: no-repeat;
  background-size: cover;
  max-width: 100%;
  height: auto;
}
</style>
