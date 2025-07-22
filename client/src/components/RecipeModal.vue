<script setup>
import { AppState } from '@/AppState.js';
import { computed, ref } from 'vue';

const account = computed(() => AppState.account)
const recipe = computed(() => AppState.activeRecipe)
const ingredient = computed(() => AppState.ingredients)
// const recipes = computed(() => AppState.recipes)

const showRecipeForm = ref({
  value: false
})

const showIngredientForm = ref({
  value: false
})

const editableRecipeData = ref({
  instructions: '',
})

const editableIngredientData = ref({
  name: '',
  quantity: '',
  recipeId: ''
})

</script>


<template>
  <div class="modal" tabindex="-1" id="recipeModal" aria-labelledby="recipeModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div v-if="recipe" class="modal-content">
        <div class="modal-header d-flex">
          <h5 class="modal-title" id="recipeModalLabel">{{ recipe.title }}
          </h5>
          <p class="fs-5 mb-0 ms-3 p-2 bg-primary-subtle rounded-5">{{ recipe.category }}</p>
          <div v-if="account && recipe.creatorId === account.id" class="dropdown justify-content-end ms-auto">
            <button class="btn btn-secondary btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown"
              aria-expanded="false">
              <i class="mdi mdi-dots-horizontal"></i>
            </button>
            <ul class="dropdown-menu">
              <li><a @click="showRecipeForm.value = true" class="dropdown-item" type="button">Edit Recipe</a></li>
              <li><a @click="showIngredientForm.value = true" class="dropdown-item" type="button">Edit Ingredient</a>
              </li>
              <li><a class="dropdown-item" type="button">Delete Recipe</a></li>
            </ul>
          </div>
        </div>
        <div class="modal-body">
          <div>
            <div class="d-flex justify-content-center">
              <img :src="recipe.img" :alt="recipe.title">
            </div>
            <div>
              <h5 class="text-center text-bold mt-1">Instructions</h5>
              <p>{{ recipe.instructions }}</p>
              <textarea v-if="showRecipeForm.value === true" v-model="editableRecipeData.instructions"
                class="form-control" rows="3" placeholder="Edit instructions here..."></textarea>
              <button v-if="account && recipe.creatorId === account.id && showRecipeForm.value === true"
                class="btn btn-primary mt-2" type="button">Save
                Instructions</button>
            </div>
            <h5 class="text-center text-bold">Ingredients</h5>
            <div v-for="ingredient in ingredient" :key="ingredient.id">
              <p>{{ ingredient.name }}</p>
              <ul>{{ ingredient.quantity }}</ul>
            </div>
            <div>
              <textarea v-if="showIngredientForm.value === true" v-model="editableIngredientData.name"
                class="form-control" rows="3" placeholder="Edit Name here..."></textarea>
              <textarea v-if="showIngredientForm.value === true" v-model="editableIngredientData.quantity"
                class="form-control" rows="3" placeholder="Edit quantity here..."></textarea>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button v-if="account && recipe.creatorId === account.id && showIngredientForm.value === true"
            class="btn btn-primary mt-2" type="button">Save
            Ingredient</button>
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
img {
  max-width: 100%;
  height: 30dvh;
}
</style>