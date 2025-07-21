<script setup>
import { Recipe } from '@/models/Recipe.js';


defineProps({
  recipe: {type: Recipe, required: true}
})


</script>


<template>
  <div>
    <div class="recipe-card">
      <div class="card-img">
        <img :src="recipe.img" :alt="`image of ${recipe.title}`" class="recipe-img" type="button">
        <div class="card-text">
          <span class="ms-2 mt-2 text-center">{{ recipe.title }}</span>
          <span class="badge rounded-3 p-1 text-center mt-1 me-1" style="max-height: 22%;">
            {{ recipe.category }}
          </span>
        </div>
        <div class="card-icon">
          <i class="mdi mdi-heart text-danger fs-4"
            type="button"></i>
          <i class="mdi mdi-heart-outline text-white fs-4" type="button"></i>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.recipe-img {
  object-fit: cover;
  width: 100%;
  height: 300px;
}

.card-img {
  position: relative;
  width: 100%;
}

.card-text {
  position: absolute;
  top: 0;
  color: white;
  font-weight: 500;
  display: flex;
  justify-content: space-between;
  width: 100%;
  text-shadow: 0 0 3px #242222;
  background: linear-gradient(180deg, #05050586 20%, transparent);
  height: 50%;
}

.card-icon {
  position: absolute;
  bottom: 0;
  display: flex;
  width: 100%;
  text-shadow: 0 0 3px #242222;
  background: linear-gradient(0deg, #1716167b 20%, transparent);
  padding: 5px;
  height: 15%;
}

.badge {
  background-color: rgba(11, 11, 11, 0.501);
}
</style>