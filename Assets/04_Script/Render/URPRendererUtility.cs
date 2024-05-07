using System.Reflection;

namespace UnityEngine.Rendering.Universal.Utility
{
    public static class URPRendererUtility
    {
        /// <summary>
        /// ���� ������ ���� ���������� ������ �����ɴϴ�.
        /// </summary>
        /// <returns>������ ���� ���������� ������ ��ȯ�մϴ�. ������ ���� ��� null�� ��ȯ�մϴ�.</returns>
        public static RenderPipelineAsset GetRenderPipelineAsset()
        {
            return GraphicsSettings.renderPipelineAsset;
        }

        /// <summary>
        /// ���� ������ ���� ���������� ������ ��ũ��Ʈ ������ ������ �����͸� �迭�� �����ɴϴ�.
        /// </summary>
        /// <returns>��ũ��Ʈ ������ ������ �������� �迭�� ��ȯ�մϴ�. ������ �����Ǿ� ���� �ʰų�, ������ ������ �迭�� ����ִ� ��� null�� ��ȯ�մϴ�.</returns>
        public static ScriptableRendererData[] GetScriptableRendererData()
        {
            RenderPipelineAsset pipelineAsset = GetRenderPipelineAsset();
            if (!pipelineAsset) return null;

            FieldInfo propertyInfo = pipelineAsset.GetType().GetField("m_RendererDataList", BindingFlags.Instance | BindingFlags.NonPublic);
            ScriptableRendererData[] scriptableRendererData = propertyInfo.GetValue(pipelineAsset) as ScriptableRendererData[];
            return scriptableRendererData;
        }

        /// <summary>
        /// ���� ������ ���� ���������� ���¿��� �־��� rendererListIndex�� UniversalRendererData�� �����ɴϴ�.
        /// </summary>
        /// <param name="rendererListIndex">UniversalRendererData�� �˻��� ������ ����� �ε����Դϴ�. �⺻���� 0�Դϴ�.</param>
        /// <returns>�־��� rendererListIndex �� UniversalRendererData �� ��ȯ�˴ϴ�. ������ �����Ǿ����� �ʰų�, ������ ������ �迭�� ����ְų�, �־��� rendererListIndex�� ������ ��� ��� null�� ��ȯ�մϴ�.</returns>
        public static UniversalRendererData GetUniversalRendererData(int rendererListIndex = 0)
        {
            ScriptableRendererData[] scriptableRendererData = GetScriptableRendererData();
            if (scriptableRendererData == null || scriptableRendererData.Length <= 0) return null;

            UniversalRendererData universalRendererData =
                scriptableRendererData[rendererListIndex] as UniversalRendererData;
            return universalRendererData;
        }

        /// <summary>
        /// ����Ʈ ���μ��� �ɼ��� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���մϴ�.
        /// </summary>
        /// <param name="universalRendererData">����Ʈ ���μ��� �����Ͱ� ���Ե� UniversalRendererData�Դϴ�.</param>
        /// <param name="renderingData">���� �������� RenderingData�Դϴ�.</param>
        /// <returns>����Ʈ ���μ����� Ȱ��ȭ�Ǿ� ������ True�̰�, �׷��� ������ False�Դϴ�.</returns>
        public static bool IsPostProcessEnabled(UniversalRendererData universalRendererData, ref RenderingData renderingData)
        {
            // RendererData ������ Post-processing Ȱ��ȭ üũ
            if (!universalRendererData ||
                !universalRendererData.postProcessData) return false;

            // ī�޶� ������Ʈ�� Post Processing Ȱ��ȭ üũ
            if (!renderingData.cameraData.postProcessEnabled)
                return false;

            return true;
        }
    }
}