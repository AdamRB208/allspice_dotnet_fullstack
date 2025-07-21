import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from "@/models/Recipe.js"
import { AppState } from "@/AppState.js"

class RecipeService {

  async getRecipes() {
    const response = await api.get('api/recipes')
    logger.log('Got Recipes!', response.data)
    const recipes = response.data.map(pojo => new Recipe(pojo))
    AppState.recipes = recipes
  }


}

export const recipeService = new RecipeService()