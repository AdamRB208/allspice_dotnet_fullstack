<script setup>
import { AppState } from '@/AppState.js';
import RecipeCard from '@/components/RecipeCard.vue';
import RecipeModal from '@/components/RecipeModal.vue';
import { favoritesService } from '@/services/FavoritesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

const favorite = computed(() => AppState.favorites)
const recipe = computed(() => AppState.recipes)

onMounted(() => {
  getMyFavorites()
})

async function getMyFavorites() {
  try {
    await favoritesService.getMyFavorites()
  }
  catch (error){
    Pop.error(error, 'COULD NOT GET FAVORITES!');
    logger.error('Could not get favorites!', error)
  }
}
</script>


<template>
  <section v-if="favorite" class="container-fluid">
    <div class="row d-flex justify-content-center">
      <div v-for="recipe in recipe" :key="recipe.id" class="col-md-3 m-2 d-flex justify-content-center flex-column">
        <RecipeCard :recipe="recipe" />
      </div>
    </div>
  </section>
  <RecipeModal>
  </RecipeModal>
</template>


<style lang="scss" scoped>

</style>