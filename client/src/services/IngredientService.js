import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Ingredient } from "@/models/Ingredient.js"
import { AppState } from "@/AppState.js"

class IngredientService {

  async getIngredientByRecipeId(recipeId) {
    const response = await api.get(`api/recipes/${recipeId}/ingredients`)
    logger.log('Got Ingredients by recipe Id', response.data)
    const ingredients = response.data.map(pojo => new Ingredient(pojo))
    AppState.ingredients = ingredients
    logger.log('rendering ingredients', ingredients)
  }

}

export const ingredientService = new IngredientService()